﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Architecture.DDD;
using Grpc.Core;
using Issues.Application.Common.Services.Files;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public class IssueCsvSeedItemService : IIssueSeedItemService
    {
        private readonly string _contentRootPath;
        private readonly ILogger<IssuesServiceDbSeed> _logger;
        private readonly ICsvFileReader _fileReader;

        public IssueCsvSeedItemService(IWebHostEnvironment env, ILogger<IssuesServiceDbSeed> logger, ICsvFileReader fileReader)
        {
            _contentRootPath = env.ContentRootPath;
            _logger = logger;
            _fileReader = fileReader;
        }
        public IEnumerable<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesFromSeed() => GetEntitiesFromFileInSetupFolder<TypeOfGroupOfIssues>("TypesOfGroupOfIssues.csv");

        public IEnumerable<GroupOfIssues> GetGroupsOfIssuesFromSeed() => GetEntitiesFromFileInSetupFolder<GroupOfIssues>("GroupsOfIssues.csv");

        public IEnumerable<Issue> GetIssuesFromSeed() => GetEntitiesFromFileInSetupFolder<Issue>("Issues.csv");

        public IEnumerable<StatusFlow> GetStatusFlowsFromSeed() => GetEntitiesFromFileInSetupFolder<StatusFlow>("StatusFlows.csv");

        public IEnumerable<StatusInFlow> GetStatusesInFlowFromSeed() => GetEntitiesFromFileInSetupFolder<StatusInFlow>("StatusesInFlow.csv");
        
        public IEnumerable<StatusInFlowConnection> GetStatusesInFlowConnectionFromSeed() => GetEntitiesFromFileInSetupFolder<StatusInFlowConnection>("StatusesInFlowConnection.csv");

        private IEnumerable<T> GetEntitiesFromFileInSetupFolder<T>(string fileName) where T : EntityBase => _fileReader.ReadEntity<T>(File.ReadAllBytes(Path.Combine(_contentRootPath, "Setup", fileName)));
    }
}
