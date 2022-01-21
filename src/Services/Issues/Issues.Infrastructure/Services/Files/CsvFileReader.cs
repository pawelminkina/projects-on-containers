using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;
using CsvHelper;
using CsvHelper.Configuration;
using Issues.Application.Common.Services.Files;
using Microsoft.Extensions.Logging;

namespace Issues.Infrastructure.Services.Files
{
    public class CsvFileReader : ICsvFileReader
    {
        private readonly ILogger<CsvFileReader> _logger;

        public CsvFileReader(ILogger<CsvFileReader> logger)
        {
            _logger = logger;
        }
        public IEnumerable<T> ReadEntity<T>(byte[] content) where T : EntityBase
        {
            var entities = new List<T>();
            using (var sm = new MemoryStream(content))
            using (var reader = new CsvReader(new StreamReader(sm), new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture) { HeaderValidated = null }))
            {
                if (reader.Context.Maps.Find<T>() is null)
                {
                    var type = GetMappingForType<T>();
                    reader.Context.RegisterClassMap(type);
                    _logger.LogInformation("Csv map for type: {type} has been registered", type);
                }

                while (reader.Read())
                {

                    var record = reader.GetRecord<T>();
                    entities.Add(record);

                }
            }

            return entities;

        }

        public Type GetMappingForType<T>()
        {
            var type = Assembly.GetAssembly(typeof(CsvFileReader)).GetTypes().FirstOrDefault(s => s.BaseType == typeof(ClassMap<T>));
            if (type == null)
                throw new InvalidOperationException("Mapping for requested type don't Exist");
            return type;
        }
    }
}
