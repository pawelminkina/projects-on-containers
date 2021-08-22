using System;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Protos;

namespace Issues.API.GrpcServices
{
    public class GrpcStatusFlowService : Protos.StatusFlowService.StatusFlowServiceBase
    {
        public override async Task<GetStatusFlowsResponse> GetStatusFlows(GetStatusFlowsRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<GetStatusFlowResponse> GetStatusFlow(GetStatusFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<AddStatusFlowResponse> AddStatusFlow(AddStatusFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<DeleteStatusFlowsResponse> DeleteStatusFlow(DeleteStatusFlowsRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<RenameStatusFlowsResponse> RenameStatusFlow(RenameStatusFlowsRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<AddStatusToFlowResponse> AddStatusToFlow(AddStatusToFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<DeleteStatusFromFlowResponse> DeleteStatusFromFlow(DeleteStatusFromFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<AddConnectionToStatusInFlowResponse> AddConnectionToStatusInFlow(AddConnectionToStatusInFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<RemoveConnectionFromStatusInFlowResponse> RemoveConnectionFromStatusInFlow(RemoveConnectionFromStatusInFlowRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}