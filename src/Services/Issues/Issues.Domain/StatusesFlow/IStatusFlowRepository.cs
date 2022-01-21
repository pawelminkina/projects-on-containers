using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public interface IStatusFlowRepository
    {
        Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId);
        Task<StatusFlow> GetFlowById(string id);
        Task<StatusInFlow> GetStatusInFlowById(string id);
        Task<IEnumerable<StatusFlow>> GetFlowsByOrganizationAsync(string organizationId);
        Task RemoveStatusInFlow(string statusInFlowId);
    }
}