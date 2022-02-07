using CsvHelper.Configuration;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Files.CsvMaps
{
    public class OrganizationMap : ClassMap<OrganizationDAO>
    {
        public OrganizationMap()
        {
            Map(s => s.Name).Name("Name");
            Map(s => s.Id).Name("Id");
            Map(s => s.Enabled).Name("Enabled");
            Map(s => s.TimeOfCreationUtc).Name("TimeOfCreationUtc");
        }
    }
}
