using Grpc.Core;
using Issues.API.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Issues.API.Extensions;
using Issues.Application.CQRS.Issues.Commands.ChangeStatus;
using Issues.Application.CQRS.Issues.Commands.CreateIssue;
using Issues.Application.CQRS.Issues.Commands.DeleteIssue;
using Issues.Application.CQRS.Issues.Commands.RenameIssue;
using Issues.Application.CQRS.Issues.Commands.UpdateIssueContent;
using Issues.Application.CQRS.Issues.Queries.GetIssuesForGroup;
using Issues.Application.CQRS.Issues.Queries.GetIssuesForUser;
using Issues.Application.CQRS.Issues.Queries.GetIssueWithContent;
using Issues.Domain.Issues;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Issues.API.GrpcServices
{
    [Authorize]
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
                context.GetUserId(), context.GetOrganizationId()));
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
            var issue = await _mediator.Send(new GetIssueWithContentQuery(request.Id, context.GetOrganizationId()));
            return new GetIssueWithContentResponse()
                {Content = MapToIssueContent(issue.Content), Issue = MapToIssueReference(issue)};
        }

        public override async Task<RenameIssueResponse> RenameIssue(RenameIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameIssueCommand(request.Id, request.NewName, context.GetOrganizationId()));
            return new RenameIssueResponse();
        }

        public override async Task<DeleteIssueResponse> DeleteIssue(DeleteIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteIssueCommand(request.Id, context.GetOrganizationId()));
            return new DeleteIssueResponse();
        }

        public override async Task<UpdateIssueTextContentResponse> UpdateIssueTextContent(UpdateIssueTextContentRequest request, ServerCallContext context)
        {
            await _mediator.Send(new UpdateIssueTextContentCommand(request.Id, request.TextContent, context.GetOrganizationId()));
            return new UpdateIssueTextContentResponse();
        }

        public override async Task<ChangeStatusOfIssueResponse> ChangeStatusOfIssue(ChangeStatusOfIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ChangeStatusOfIssueCommand(request.IssueId, request.NewStatusInFlowId, context.GetOrganizationId()));
            return new ChangeStatusOfIssueResponse();
        }


        private Protos.IssueReference MapToIssueReference(Issue issue) => new IssueReference()
        {
            CreatingUserId = issue.CreatingUserId,
            Id = issue.Id,
            Name = issue.Name,
            TimeOfCreation = issue.TimeOfCreation.ToTimestamp(),
            GroupId = issue.GroupOfIssue.Id,
            IsDeleted = issue.IsDeleted,
            StatusName = issue.StatusInFlow.Name
        };

        private Protos.IssueContent MapToIssueContent(Domain.Issues.IssueContent content) => new Protos.IssueContent()
        {
            TextContent = content.TextContent
        };
    }
}
