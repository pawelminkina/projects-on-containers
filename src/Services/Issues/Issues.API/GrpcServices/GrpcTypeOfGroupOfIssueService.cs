using System;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Protos;

namespace Issues.API.GrpcServices
{
    public class GrpcTypeOfGroupOfIssueService : Protos.TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceBase
    {
        public GrpcTypeOfGroupOfIssueService()
        {
                
        }
        public override async Task<GetTypesOfGroupsOfIssuesResponse> GetTypesOfGroupsOfIssues(GetTypesOfGroupsOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<GetTypeOfGroupOfIssuesResponse> GetTypeOfGroupOfIssues(GetTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<CreateTypefOfGroupOfIssuesResponse> CreateTypefOfGroupOfIssues(CreateTypefOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<DeleteTypeOfGroupOfIssuesResponse> DeleteTypeOfGroupOfIssues(DeleteTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            return await base.DeleteTypeOfGroupOfIssues(request, context);
        }

        public override async Task<RenameTypeOfGroupOfIssuesResponse> RenameTypeOfGroupOfIssues(RenameTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            return await base.RenameTypeOfGroupOfIssues(request, context);
        }
    }
}