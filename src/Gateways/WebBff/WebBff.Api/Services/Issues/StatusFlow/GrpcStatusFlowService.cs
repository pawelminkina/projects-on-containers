using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.StatusFlow;
using AddStatusToFlowRequest = Issues.API.Protos.AddStatusToFlowRequest;

namespace WebBff.Api.Services.Issues.StatusFlows
{
    public class GrpcStatusFlowService : IStatusFlowService
    {
        private readonly StatusFlowService.StatusFlowServiceClient _client;

        public GrpcStatusFlowService(StatusFlowService.StatusFlowServiceClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<StatusFlowDto>> GetStatusFlowsAsync()
        {
            var res = await _client.GetStatusFlowsAsync(new GetStatusFlowsRequest());
            return res.Flow.Select(MapToDto);
        }

        public async Task<StatusFlowDto> GetStatusFlowAsync(string id)
        {
            var res = await _client.GetStatusFlowAsync(new GetStatusFlowRequest() { Id = id });
            return MapToDto(res.Flow);
        }

        public async Task<string> CreateStatusFlowAsync(CreateStatusFlowRequest request)
        {
            var res = await _client.AddStatusFlowAsync(new AddStatusFlowRequest() { Name = request.Name});
            return res.Id;
        }

        public async Task DeleteStatusFlowAsync(string id)
        {
            var res = await _client.DeleteStatusFlowAsync(new DeleteStatusFlowsRequest() { Id = id });
        }

        public async Task RenameStatusFlowAsync(string id, string newName)
        {
            var res = await _client.RenameStatusFlowAsync(new RenameStatusFlowsRequest() { Id = id, Name = newName});
        }

        public async Task AddStatusToFlowAsync(string statusId, string flowId)
        {
            var res = await _client.AddStatusToFlowAsync(new AddStatusToFlowRequest() { FlowId = flowId, StatusId = statusId});
        }

        public async Task DeleteStatusFromFlowAsync(string statusId, string flowId)
        {
            var res = await _client.DeleteStatusFromFlowAsync(new DeleteStatusFromFlowRequest() { StatusId = statusId, FlowId = flowId});
        }

        public async Task AddConnectionToStatusInFlowAsync(string statusId, string statusToConnectId, string flowId)
        {
            var res = await _client.AddConnectionToStatusInFlowAsync(new AddConnectionToStatusInFlowRequest() { FlowId = flowId, ParentStatusId = statusId, ConnectedStatusId = statusToConnectId});
        }

        public async Task DeleteConnectionToStatusFromFlowAsync(string statusId, string flowId)
        {
            var res = await _client.DeleteStatusFromFlowAsync(new DeleteStatusFromFlowRequest() { FlowId = flowId, StatusId = statusId});
        }

        private StatusFlowDto MapToDto(StatusFlow flow) => new StatusFlowDto()
        {
            Id = flow.Id,
            Name = flow.Name,
            Statuses = flow.Statuses.Select(d => new Models.Issuses.StatusFlow.StatusInFlow() { ChildStatusIds = d.ConnectedStatuses.Select(s=>s.ConnectedStatusId), IndexInFlow = d.IndexInFLow, ParentStatusId = d.ParentStatusId })
        };
    }
}