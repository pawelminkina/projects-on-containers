using System.Reflection;
using Architecture.DDD;
using CsvHelper;
using CsvHelper.Configuration;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Files
{
    public class CsvFileReader
    {
        public IEnumerable<T> ReadEntity<T>(byte[] content)
        {
            var entities = new List<T>();
            using (var sm = new MemoryStream(content))
            using (var reader = new CsvReader(new StreamReader(sm), new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture) { HeaderValidated = null }))
            {
                var csvDtoMapType = GetMappingForType<T>();

                reader.Context.RegisterClassMap(csvDtoMapType);

                var currentCsvType = typeof(T);
                while (reader.Read())
                {
                    var recordAsCsvDto = reader.GetRecord(currentCsvType);
                    var recordAsEntity = (T)recordAsCsvDto;
                    entities.Add(recordAsEntity);
                }
            }

            return entities;

        }

        public Type GetMappingForType<T>()
        {
            var csvDtoType =typeof(T);
            var type = Assembly.GetAssembly(typeof(CsvFileReader))?.GetTypes().FirstOrDefault(s => s.BaseType.GenericTypeArguments.Any(d => d == csvDtoType));
            if (type == null)
                throw new InvalidOperationException("Mapping for requested type don't Exist");
            return type;
        }
    }
}
