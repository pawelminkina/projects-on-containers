using CsvHelper.Configuration;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Files.CsvMaps
{
    public class UsersMap : ClassMap<UserDAO>
    {
        public UsersMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.Email).Name("Email");
            Map(s => s.UserName).Name("UserName");
            Map(s => s.NormalizedEmail).Name("NormalizedEmail");
            Map(s => s.NormalizedUserName).Name("NormalizedUserName");
            Map(s => s.SecurityStamp).Name("SecurityStamp");
            Map(s => s.TimeOfCreationUtc).Name("TimeOfCreationUtc");
            Map(s => s.Fullname).Name("Fullname");
            Map(s => s.PasswordHash).Name("PasswordHash");
            Map(s => s.OrganizationId).Name("OrganizationId");
        }
    }
}
