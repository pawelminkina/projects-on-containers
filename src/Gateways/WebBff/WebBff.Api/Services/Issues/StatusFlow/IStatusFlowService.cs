using System.Collections.Generic;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.StatusFlow;

namespace WebBff.Api.Services.Issues.StatusFlows
{
    public interface IStatusFlowService
    {
        Task<IEnumerable<StatusFlowDto>> GetStatusFlowsAsync();
        Task<StatusFlowDto> GetStatusFlowAsync(string id);
        Task<string> CreateStatusFlowAsync(CreateStatusFlowRequest request);
        Task AddStatusToFlowAsync(string statusId, string flowId);
        Task AddConnectionToStatusInFlowAsync(string statusId, string statusToConnectId, string flowId);
        Task RenameStatusFlowAsync(string id, string newName);
        Task DeleteStatusFlowAsync(string id);
        Task DeleteStatusFromFlowAsync(string statusId, string flowId);
        Task DeleteConnectionToStatusFromFlowAsync(string statusId, string flowId);
    }
}