using Grpc.Core;
using Issues.API.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Issues.API.Extensions;
using Issues.Application.Issues.CreateIssue;
using Issues.Application.Issues.GetIssuesForGroup;
using Issues.Application.Issues.GetIssuesForUser;
using Issues.Application.Issues.GetIssueWithContent;
using Issues.Application.Issues.RenameIssue;
using Issues.Application.Issues.UpdateIssueContent;
using Issues.Domain.Issues;
using MediatR;
using Issues.Application.Issues.DeleteIssue;

namespace Issues.API.GrpcServices
{
    //TODO Proto services
    //TODO create commands and queries with fluent validators, if validator exist add unit test for him
    //TODO webBff
    //TODO Acceptance tests for queries and commands
    //TODO User service
    //TODO Auth service
    public class GrpcIssueService : Protos.IssueService.IssueServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcIssueService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public override async Task<CreateIssueResponse> CreateIssue(CreateIssueRequest request, ServerCallContext context)
        {
            var id = await _mediator.Send(new CreateIssueCommand(request.Name, request.GroupId, request.TextContent,
                context.GetUserId(), context.GetOrganizationId(), request.TypeOfIssueId));
            return new CreateIssueResponse() {Id = id};
        }

        public override async Task<GetIssuesForGroupResponse> GetIssuesForGroup(GetIssuesForGroupRequest request, ServerCallContext context)
        {
            var issues = await _mediator.Send(new GetIssuesForGroupQuery(request.GroupId, context.GetOrganizationId()));
            var result = new GetIssuesForGroupResponse();
            result.Issues.AddRange(issues.Select(MapToIssueReference));
            return result;
        }

        public override async Task<GetIssuesForUserResponse> GetIssuesForUser(GetIssuesForUserRequest request, ServerCallContext context)
        {
            var issues = await _mediator.Send(new GetIssuesForUserQuery(request.UserId));
            var result = new GetIssuesForUserResponse();
            result.Issues.AddRange(issues.Select(MapToIssueReference));
            return result;
        }

        public override async Task<GetIssueWithContentResponse> GetIssueWithContent(GetIssueWithContentRequest request, ServerCallContext context)
        {
            var issue = await _mediator.Send(new GetIssueWithContentQuery(request.IssueId, context.GetOrganizationId()));
            return new GetIssueWithContentResponse()
                {Content = MapToIssueContent(issue.Content), Issue = MapToIssueReference(issue)};
        }

        public override async Task<RenameIssueResponse> RenameIssue(RenameIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameIssueCommand(request.Id, request.NewName, context.GetOrganizationId()));
            return new RenameIssueResponse();
        }

        public override async Task<UpdateIssueContentResponse> UpdateIssueContent(UpdateIssueContentRequest request, ServerCallContext context)
        {
            await _mediator.Send(new UpdateIssueContentCommand(request.IssueId, request.TextContent,
                context.GetOrganizationId()));
            return new UpdateIssueContentResponse();
        }

        public override async Task<DeleteIssueResponse> DeleteIssue(DeleteIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteIssueCommand(request.Id, context.GetOrganizationId()));
            return new DeleteIssueResponse();
        }


        private Protos.IssueReference MapToIssueReference(Issue issue) => new IssueReference()
        {
            CreatingUserId = issue.CreatingUserId,
            Id = issue.Id,
            Name = issue.Name,
            StatusId = issue.StatusId,
            TimeOfCreation = issue.TimeOfCreation.ToTimestamp(),
            TypeOfIssueId = issue.TypeOfIssue.Id,
            IsArchived = issue.IsArchived,
            GroupId = issue.GroupOfIssue.Id,
            IsDeleted = issue.IsDeleted
        };

        private Protos.IssueContent MapToIssueContent(Domain.Issues.IssueContent content) => new Protos.IssueContent()
        {
            TextContent = content.TextContent
        };
    }
}
