using System.Collections.Generic;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.StatusFlow;
using AddStatusToFlowRequest = Issues.API.Protos.AddStatusToFlowRequest;

namespace WebBff.Api.Services.Issues.StatusFlow
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
            return null;
        }

        public async Task<StatusFlowDto> GetStatusFlowAsync(string id)
        {
            var res = await _client.GetStatusFlowAsync(new GetStatusFlowRequest());
            return null;
        }

        public async Task<string> CreateStatusFlowAsync(StatusFlowDto flow)
        {
            var res = await _client.AddStatusFlowAsync(new AddStatusFlowRequest());
            return string.Empty;
        }

        public async Task DeleteStatusFlowAsync(string id)
        {
            var res = await _client.DeleteStatusFlowAsync(new DeleteStatusFlowsRequest());
        }

        public async Task RenameStatusFlowAsync(string id, string newName)
        {
            var res = await _client.RenameStatusFlowAsync(new RenameStatusFlowsRequest());
        }

        public async Task AddStatusToFlowAsync(string statusId, string flowId)
        {
            var res = await _client.AddStatusToFlowAsync(new AddStatusToFlowRequest());
        }

        public async Task DeleteStatusFromFlowAsync(string statusId, string flowId)
        {
            var res = await _client.DeleteStatusFromFlowAsync(new DeleteStatusFromFlowRequest());
        }

        public async Task AddConnectionToStatusInFlowAsync(string statusId, string statusToConnectId, string flowId)
        {
            var res = await _client.AddConnectionToStatusInFlowAsync(new AddConnectionToStatusInFlowRequest());
        }

        public async Task DeleteConnectionToStatusFromFlowAsync(string statusId, string connectedStatusId, string flowId)
        {
            var res = await _client.DeleteStatusFromFlowAsync(new DeleteStatusFromFlowRequest());
        }
    }
}