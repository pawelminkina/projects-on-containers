using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public interface IStatusFlowRepository
    {
        Task<StatusFlow> AddNewStatusFlowAsync(StatusFlow statusFlow);
        Task<StatusFlow> GetFlowById(string id);
        Task<StatusInFlow> GetStatusInFlowById(string id);
        Task<IEnumerable<StatusFlow>> GetFlowsByOrganizationAsync(string organizationId);
        Task<StatusFlow> GetDefaultStatusFlowAsync(string organizationId);
        Task RemoveStatusInFlow(string statusInFlowId);
    }
}