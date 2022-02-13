using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.CreateType;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.DeleteType;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.RenameType;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Queries.GetType;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Queries.GetTypes;
using MediatR;
using Status = Grpc.Core.Status;

namespace Issues.API.GrpcServices
{
    public class GrpcTypeOfGroupOfIssueService : Protos.TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcTypeOfGroupOfIssueService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GetTypesOfGroupsOfIssuesResponse> GetTypesOfGroupsOfIssues(GetTypesOfGroupsOfIssuesRequest request, ServerCallContext context)
        {
            var typesOfGroupsOfIssues = await _mediator.Send(new GetTypesOfGroupOfIssuesQuery(context.GetOrganizationId()));
            if (!typesOfGroupsOfIssues.Any())
                return new GetTypesOfGroupsOfIssuesResponse();

            var resToReturn = new GetTypesOfGroupsOfIssuesResponse();
            resToReturn.TypesOfGroups.AddRange(typesOfGroupsOfIssues.Select(MapToTypeOfGroupOfIssues));
            return resToReturn;
        }

        public override async Task<GetTypeOfGroupOfIssuesResponse> GetTypeOfGroupOfIssues(GetTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            var typeOfGroupOfIssues = await _mediator.Send(new GetTypeOfGroupOfIssuesQuery(request.Id, context.GetOrganizationId()));
            return new GetTypeOfGroupOfIssuesResponse() {TypeOfGroup = MapToTypeOfGroupOfIssues(typeOfGroupOfIssues)};
        }

        public override async Task<CreateTypeOfGroupOfIssuesResponse> CreateTypeOfGroupOfIssues(CreateTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            var idOfTypeOfGroupOfIssues = await _mediator.Send(new CreateTypeOfGroupOfIssuesCommand(request.Name, context.GetOrganizationId()));
            return new CreateTypeOfGroupOfIssuesResponse() {Id = idOfTypeOfGroupOfIssues};
        }

        public override async Task<RenameTypeOfGroupOfIssuesResponse> RenameTypeOfGroupOfIssues(RenameTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameTypeOfGroupOfIssuesCommand(request.Id, context.GetOrganizationId(), request.NewName));
            return new RenameTypeOfGroupOfIssuesResponse();
        }

        public override async Task<DeleteTypeOfGroupOfIssuesResponse> DeleteTypeOfGroupOfIssues(DeleteTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteTypeOfGroupOfIssuesCommand(request.Id, context.GetOrganizationId()));
            return new DeleteTypeOfGroupOfIssuesResponse();
        }



        private Protos.TypeOfGroupOfIssues MapToTypeOfGroupOfIssues(Domain.GroupsOfIssues.TypeOfGroupOfIssues type) =>
            new TypeOfGroupOfIssues() {Id = type.Id, Name = type.Name, IsDefault = type.IsDefault};
    }
}