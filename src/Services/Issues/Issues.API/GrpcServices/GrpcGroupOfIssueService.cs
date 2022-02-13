using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using MediatR;
using Google.Protobuf.WellKnownTypes;
using Issues.Application.CQRS.GroupOfIssues.Commands.ChangeShortNameInGroup;
using Issues.Application.CQRS.GroupOfIssues.Commands.CreateGroup;
using Issues.Application.CQRS.GroupOfIssues.Commands.DeleteGroup;
using Issues.Application.CQRS.GroupOfIssues.Commands.RenameGroup;
using Issues.Application.CQRS.GroupOfIssues.Queries.GetGroup;
using Issues.Application.CQRS.GroupOfIssues.Queries.GetGroupsForOrganization;
using Status = Grpc.Core.Status;

namespace Issues.API.GrpcServices
{
    public class GrpcGroupOfIssueService : Protos.GroupOfIssueService.GroupOfIssueServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcGroupOfIssueService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CreateGroupOfIssuesResponse> CreateGroupOfIssues(CreateGroupOfIssuesRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new CreateGroupOfIssuesCommand(request.TypeOfGroupId, request.Name, request.ShortName, context.GetOrganizationId()));
            return new CreateGroupOfIssuesResponse() {Id = result};
        }

        public override async Task<GetGroupOfIssuesResponse> GetGroupOfIssues(GetGroupOfIssuesRequest request, ServerCallContext context)
        {
            var group = await _mediator.Send(new GetGroupOfIssuesQuery(request.Id, context.GetOrganizationId()));
            return new GetGroupOfIssuesResponse() {Group = MapToGrpcGroup(group)};
        }

        public override async Task<GetGroupsOfIssuesResponse> GetGroupsOfIssues(GetGroupsOfIssuesRequest request, ServerCallContext context)
        {
            var groups = await _mediator.Send(new GetGroupsOfIssuesForOrganizationQuery(context.GetOrganizationId()));
            
            if (groups.Any())
            {
                var res = new GetGroupsOfIssuesResponse();
                res.Groups.AddRange(groups.Select(MapToGrpcGroup));
                return res;
            }

            return new GetGroupsOfIssuesResponse();
        }

        public override async Task<RenameGroupOfIssuesResponse> RenameGroupOfIssues(RenameGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameGroupOfIssuesCommand(request.Id, request.NewName, context.GetOrganizationId()));
            return new RenameGroupOfIssuesResponse();
        }

        public override async Task<ChangeShortNameForGroupOfIssuesResponse> ChangeShortNameForGroupOfIssues(ChangeShortNameForGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ChangeShortNameInGroupOfIssuesCommand(request.Id, request.NewShortName, context.GetOrganizationId()));
            return new ChangeShortNameForGroupOfIssuesResponse();
        }

        public override async Task<DeleteGroupOfIssuesResponse> DeleteGroupOfIssues(DeleteGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteGroupOfIssueCommand(request.Id, context.GetOrganizationId()));
            return new DeleteGroupOfIssuesResponse();
        }

        private GroupOfIssue MapToGrpcGroup(Domain.GroupsOfIssues.GroupOfIssues group) => new GroupOfIssue()
            {Id = group.Id, Name = group.Name, TypeOfGroupId = group.TypeOfGroup.Id, ShortName = group.ShortName, IsDeleted = group.IsDeleted, TimeOfDelete = group.TimeOfDeleteUtc?.ToTimestamp()};
    }
}