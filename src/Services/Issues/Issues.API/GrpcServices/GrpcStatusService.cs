using System;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Protos;

namespace Issues.API.GrpcServices
{
    public class GrpcStatusService : Protos.StatusService.StatusServiceBase
    {
        public override async Task<GetStatusesResponse> GetStatuses(GetStatusesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        public override async Task<GetStatusResponse> GetStatus(GetStatusRequest request, ServerCallContext context)
        {
            return await base.GetStatus(request, context);
        }

        public override async Task<CreateStatusResponse> CreateStatus(CreateStatusRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<ArchiveStatusResponse> ArchiveStatus(ArchiveStatusRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<RenameStatusResponse> RenameStatus(RenameStatusRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}