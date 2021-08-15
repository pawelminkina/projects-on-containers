using Grpc.Core;
using Issues.API.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issues.API.GrpcServices
{
    public class GrpcIssueService : Protos.IssueService.IssueServiceBase
    {
        public override Task<CreateIssueResponse> CreateIssue(CreateIssueRequest request, ServerCallContext context)
        {
            return base.CreateIssue(request, context);
        }

        public override Task<DeleteIssueResponse> DeleteIssue(DeleteIssueRequest request, ServerCallContext context)
        {
            return base.DeleteIssue(request, context);
        }

        public override Task<GetIssuesForGroupResponse> GetIssuesForGroup(GetIssuesForGroupRequest request, ServerCallContext context)
        {
            return base.GetIssuesForGroup(request, context);
        }

        public override Task<GetIssuesForUserResponse> GetIssuesForUser(GetIssuesForUserRequest request, ServerCallContext context)
        {
            return base.GetIssuesForUser(request, context);
        }

        public override Task<GetIssueWithContentResponse> GetIssueWithContent(GetIssueWithContentRequest request, ServerCallContext context)
        {
            return base.GetIssueWithContent(request, context);
        }

        public override Task<RenameIssueResponse> RenameIssue(RenameIssueRequest request, ServerCallContext context)
        {
            return base.RenameIssue(request, context);
        }

        public override Task<UpdateIssueContentResponse> UpdateIssueContent(UpdateIssueContentRequest request, ServerCallContext context)
        {
            return base.UpdateIssueContent(request, context);
        }

        public override Task<UpdateIssueStatusResponse> UpdateIssueStatus(UpdateIssueStatusRequest request, ServerCallContext context)
        {
            return base.UpdateIssueStatus(request, context);
        }
    }
}
