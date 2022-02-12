using Issues.API.Protos;
using WebBff.Aggregator.Models.StatusFlow;

namespace WebBff.Aggregator.Services.StatusFlow;

public class GrpcStatusFlowService : IStatusFlowService
{
    private readonly StatusFlowService.StatusFlowServiceClient _grpcClient;

    public GrpcStatusFlowService(StatusFlowService.StatusFlowServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<IEnumerable<StatusFlowDto>> GetStatusFlows()
    {
        var response = await _grpcClient.GetStatusFlowsAsync(new GetStatusFlowsRequest());
        return response.Flows.Select(MapToStatusFlowDto);
    }

    public async Task<StatusFlowWithStatusesDto> GetStatusFlowWithStatuses(string id)
    {
        var response = await _grpcClient.GetStatusFlowAsync(new GetStatusFlowRequest() {Id = id});
        return MapToStatusFlowWithStatusesDto(response.Flow);
    }

    public async Task<StatusFlowWithStatusesDto> GetStatusFlowForGroupOfIssues(string groupOfIssuesId)
    {
        var response = await _grpcClient.GetStatusFlowForGroupOfIssuesAsync(new GetStatusFlowForGroupOfIssuesRequest() { GroupOfIssuesId = groupOfIssuesId });
        return MapToStatusFlowWithStatusesDto(response.Flow);
    }

    public async Task AddStatusToFlow(AddStatusToFlowDto dto)
    {
        await _grpcClient.AddStatusToFlowAsync(new AddStatusToFlowRequest() {FlowId = dto.FlowId, StatusName = dto.StatusName});
    }

    public async Task DeleteStatusFromFlow(string statusInFlowId)
    {
        await _grpcClient.DeleteStatusFromFlowAsync(new DeleteStatusFromFlowRequest(){StatusInFlowId = statusInFlowId});
    }

    public async Task AddConnectionToStatusInFlow(string parentStatusInFlowId, string connectedStatusInFlowId)
    {
        await _grpcClient.AddConnectionToStatusInFlowAsync(new AddConnectionToStatusInFlowRequest() {ConnectedStatusInFlowId = connectedStatusInFlowId, ParentStatusinFlowId = parentStatusInFlowId});
    }

    public async Task RemoveConnectionFromStatusInFlow(string parentStatusInFlowId, string connectedStatusInFlowId)
    {
        await _grpcClient.RemoveConnectionFromStatusInFlowAsync(new RemoveConnectionFromStatusInFlowRequest() { ConnectedStatusInFlowId = connectedStatusInFlowId, ParentStatusinFlowId = parentStatusInFlowId });
    }

    public async Task ChangeDefaultStatusInFlow(string newStatusInFlowId)
    {
        await _grpcClient.ChangeDefaultStatusInFlowAsync(new ChangeDefaultStatusInFlowRequest() {NewDefaultStatusInFlowId = newStatusInFlowId});
    }

    private StatusFlowDto MapToStatusFlowDto(global::Issues.API.Protos.StatusFlow statusFlow)
    {
        return new StatusFlowDto()
        {
            Id = statusFlow.Id,
            IsDefault = statusFlow.IsDefault,
            IsDeleted = statusFlow.IsDeleted,
            Name = statusFlow.Name
        };
    }

    private StatusFlowWithStatusesDto MapToStatusFlowWithStatusesDto(global::Issues.API.Protos.StatusFlow statusFlow)
    {
        return new StatusFlowWithStatusesDto(MapToStatusFlowDto(statusFlow))
        {
            Statuses = statusFlow.Statuses.Select(s=> new StatusInFlowDto()
            {
                Id = s.Id,
                IsDefault = s.IsDefault,
                Name = s.Name,
                ConnectedStatusesIds = s.ConnectedStatusesId
            })
        };
    }
}