using System;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Protos;

namespace Issues.API.GrpcServices
{
    public class GrpcGroupOfIssueService : Protos.GroupOfIssueService.GroupOfIssueServiceBase
    {
        public GrpcGroupOfIssueService()
        {
            
        }
        //again, probably i will code at 7.09.2021, but i will push this empty commits co keep github green for the whole year
        //is it cheating? Please hr tell me
        public override async Task<ArchiveGroupOfIssuesResponse> ArchiveGroupOfIssues(ArchiveGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public async override Task<CreateGroupOfIssuesResponse> CreateGroupOfIssues(CreateGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public async override Task<GetGroupOfIssuesResponse> GetGroupOfIssues(GetGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public async override Task<GetGroupsOfIssuesResponse> GetGroupsOfIssues(GetGroupsOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public async override Task<RenameGroupOfIssuesResponse> RenameGroupOfIssues(RenameGroupOfIssuesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}