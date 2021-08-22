using System;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Protos;

namespace Issues.API.GrpcServices
{
    public class GrpcTypeOfIssueService : Protos.TypeOfIssueService.TypeOfIssueServiceBase
    {
        public override async Task<GetTypesOfIssuesResponse> GetTypesOfIssues(GetTypesOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<GetTypeOfIssueResponse> GetTypeOfIssue(GetTypeOfIssueRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<CreateTypeOfIssueResponse> CreateTypeOfIssue(CreateTypeOfIssueRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<DeleteTypeOfIssueResponse> DeleteTypeOfIssue(DeleteTypeOfIssueRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<RenameTypeOfIssueResponse> RenameTypeOfIssue(RenameTypeOfIssueRequest request, ServerCallContext context)
        {
            return await base.RenameTypeOfIssue(request, context);
        }
    }
}