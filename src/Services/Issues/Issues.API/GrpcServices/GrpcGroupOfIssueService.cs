using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.GroupOfIssues.ArchiveGroup;
using Issues.Application.GroupOfIssues.CreateGroup;
using Issues.Application.GroupOfIssues.GetGroup;
using Issues.Application.GroupOfIssues.GetGroupsForOrganization;
using Issues.Application.GroupOfIssues.RenameGroup;
using MediatR;
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
        public override async Task<ArchiveGroupOfIssuesResponse> ArchiveGroupOfIssues(ArchiveGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ArchiveGroupOfIssuesCommand(request.Id, context.GetOrganizationId()));
            return new ArchiveGroupOfIssuesResponse();
        }

        public override async Task<CreateGroupOfIssuesResponse> CreateGroupOfIssues(CreateGroupOfIssuesRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new CreateGroupOfIssuesCommand(request.TypeOfGroupId, request.Name, request.ShortName, context.GetOrganizationId()));
            return new CreateGroupOfIssuesResponse() {Id = result};
        }

        public override async Task<GetGroupOfIssuesResponse> GetGroupOfIssues(GetGroupOfIssuesRequest request, ServerCallContext context)
        {
            var group = await _mediator.Send(new GetGroupOfIssuesQuery(request.Id, context.GetOrganizationId()));
            if (group is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Group of issues with id: {request.Id} was not found"));
            
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

        private GroupOfIssue MapToGrpcGroup(Domain.GroupsOfIssues.GroupOfIssues group) => new GroupOfIssue()
            {Id = group.Id, Name = group.Name, TypeOfGroupId = group.TypeOfGroupId, ShortName = group.ShortName, IsArchived = group.IsArchived};
    }
}