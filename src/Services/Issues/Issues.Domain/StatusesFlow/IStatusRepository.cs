using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public interface IStatusRepository
    {
        Task<Status> AddNewStatusAsync(string name, string organizationId);
        Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId);
        Task<Status> GetStatusById(string id);
        Task<IEnumerable<Status>> GetStatusesForOrganization(string organizationId);
        Task<StatusFlow> GetFlowById(string id);
        Task<IEnumerable<StatusFlow>> GetFlowsByOrganization(string organizationId);
    }
}