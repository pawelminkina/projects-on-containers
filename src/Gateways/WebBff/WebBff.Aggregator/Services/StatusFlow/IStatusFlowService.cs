using WebBff.Aggregator.Models.StatusFlow;

namespace WebBff.Aggregator.Services.StatusFlow
{
    public interface IStatusFlowService
    {
        Task<IEnumerable<StatusFlowDto>> GetStatusFlows();
        Task<StatusFlowWithStatusesDto> GetStatusFlowWithStatuses(string id);
        Task<StatusFlowWithStatusesDto> GetStatusFlowForGroupOfIssues(string groupOfIssuesId);
        Task AddStatusToFlow(AddStatusToFlowDto dto);
        Task DeleteStatusFromFlow(string statusInFlowId);
        Task AddConnectionToStatusInFlow(string parentStatusInFlowId, string connectedStatusInFlowId);
        Task RemoveConnectionFromStatusInFlow(string parentStatusInFlowId, string connectedStatusInFlowId);
        Task ChangeDefaultStatusInFlow(string newStatusInFlowId);
    }
}
