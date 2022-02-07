using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Database
{
    public interface IUserSeedItemService
    {
        IEnumerable<OrganizationDAO> GetOrganizationsFromSeed();

    }
}
