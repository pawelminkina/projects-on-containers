using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    //TODO this should be seperated into 2 repositories because there are 2 roots
    public interface IStatusRepository
    {
        Task<Status> AddNewStatusAsync(string name, string organizationId);
        Task<Status> GetStatusById(string id);
        Task<IEnumerable<Status>> GetStatusesForOrganization(string organizationId);
        Task RemoveStatusById(string id);
    }
}