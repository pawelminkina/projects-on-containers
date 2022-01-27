using Architecture.DDD;
using CsvHelper;
using CsvHelper.Configuration;
using Issues.Application.Common.Models.Files.Csv;
using Issues.Application.Common.Services.Files;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Issues.Domain;

namespace Issues.Infrastructure.Services.Files
{
    public class CsvFileReader : ICsvFileReader
    {
        private readonly ILogger<CsvFileReader> _logger;
        private Dictionary<Type, Type> _entityBase;

        public CsvFileReader(ILogger<CsvFileReader> logger)
        {
            _logger = logger;
            SetupMaps();
        }
        public IEnumerable<T> ReadEntity<T>(byte[] content) where T : EntityBase
        {
            var entities = new List<T>();
            using (var sm = new MemoryStream(content))
            using (var reader = new CsvReader(new StreamReader(sm), new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture) { HeaderValidated = null }))
            {
                var csvDtoMapType = GetMappingForType<T>();

                reader.Context.RegisterClassMap(csvDtoMapType);
                _logger.LogInformation("Csv map for type: {type} has been registered", csvDtoMapType);

                var currentCsvType = _entityBase.GetValueOrDefault(typeof(T));
                while (reader.Read())
                {
                    var recordAsCsvDto = reader.GetRecord(currentCsvType);
                    var recordAsEntity = MapTypeToEntity(recordAsCsvDto) as T;
                    entities.Add(recordAsEntity);
                }
            }

            return entities;

        }

        public Type GetMappingForType<T>()
        {
            var csvDtoType = _entityBase.GetValueOrDefault(typeof(T));
            var type = Assembly.GetAssembly(typeof(CsvFileReader))?.GetTypes().FirstOrDefault(s => s.BaseType.GenericTypeArguments.Any(d => d == csvDtoType));
            if (type == null)
                throw new InvalidOperationException("Mapping for requested type don't Exist");
            return type;
        }

        public void SetupMaps()
        {
            _entityBase = new Dictionary<Type, Type>()
            {
                {typeof(GroupOfIssues), typeof(GroupOfIssuesCvsDto)},
                {typeof(TypeOfGroupOfIssues), typeof(TypeOfGroupOfIssuesCsvDto)},
                {typeof(StatusInFlow), typeof(StatusInFlowCsvDto)},
                {typeof(StatusInFlowConnection), typeof(StatusInFlowConnectionCsvDto)},
                {typeof(StatusFlow), typeof(StatusFlowCsvDto)},
                {typeof(Issue), typeof(IssueCsvDto)},
            };
        }

        private object MapTypeToEntity(object recordAsCsvDto)
        {
            if (recordAsCsvDto is GroupOfIssuesCvsDto groupOfIssues)
                return WholeEntityObjectCreator.CreateGroupOfIssues(groupOfIssues.Id,groupOfIssues.Name,groupOfIssues.ShortName,groupOfIssues.TypeOfGroupId,groupOfIssues.ConnectedStatusFlowId,groupOfIssues.IsDeleted,groupOfIssues.TimeOfDeleteUtc);

            else if (recordAsCsvDto is TypeOfGroupOfIssuesCsvDto typeOfGroupOfIssues)
                return WholeEntityObjectCreator.CreateTypeOfGroupOfIssues(typeOfGroupOfIssues.Id,typeOfGroupOfIssues.Name,typeOfGroupOfIssues.OrganizationId,typeOfGroupOfIssues.IsDefault);

            else if (recordAsCsvDto is IssueCsvDto issue)
                return WholeEntityObjectCreator.CreateIssue(issue.Id,issue.Name,issue.CreatingUserId,issue.GroupOfIssueId,issue.TimeOfCreation,issue.TextContent,issue.IsDeleted,issue.StatusInFlowId);

            else if (recordAsCsvDto is StatusFlowCsvDto statusFlow)
                return WholeEntityObjectCreator.CreateStatusFlow(statusFlow.Id,statusFlow.Name,statusFlow.OrganizationId,statusFlow.IsDefault,statusFlow.IsDeleted);

            else if (recordAsCsvDto is StatusInFlowCsvDto statusInFlow)
                return WholeEntityObjectCreator.CreateStatusInFlow(statusInFlow.Id,statusInFlow.StatusFlowId,statusInFlow.Name,statusInFlow.IsDefault);

            else if (recordAsCsvDto is StatusInFlowConnectionCsvDto statusInFlowConnection)
                return WholeEntityObjectCreator.CreateStatusInFlowConnection(statusInFlowConnection.Id,statusInFlowConnection.ParentStatusInFlowId,statusInFlowConnection.ConnectedStatusInFlowId,statusInFlowConnection.Direction);

            throw new InvalidOperationException($"Requested type of entity: {recordAsCsvDto.GetType()} has no mapping added");
        }
    }
}
