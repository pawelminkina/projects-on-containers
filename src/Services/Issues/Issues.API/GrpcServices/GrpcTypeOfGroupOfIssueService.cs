using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.TypeOfGroupOfIssues.ArchiveType;
using Issues.Application.TypeOfGroupOfIssues.CreateType;
using Issues.Application.TypeOfGroupOfIssues.GetType;
using Issues.Application.TypeOfGroupOfIssues.GetTypes;
using Issues.Application.TypeOfGroupOfIssues.RenameType;
using MediatR;

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
            resToReturn.Types_.AddRange(typesOfGroupsOfIssues.Select(MapToTypeOfGroupOfIssues));
            return resToReturn;
        }

        public override async Task<GetTypeOfGroupOfIssuesResponse> GetTypeOfGroupOfIssues(GetTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            var typeOfGroupOfIssues = await _mediator.Send(new GetTypeOfGroupOfIssuesQuery(request.Id, context.GetOrganizationId()));
            if (typeOfGroupOfIssues is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Type of group of issues with id: {request.Id} was not found"));

            return new GetTypeOfGroupOfIssuesResponse() {Type = MapToTypeOfGroupOfIssues(typeOfGroupOfIssues)};
        }

        public override async Task<CreateTypefOfGroupOfIssuesResponse> CreateTypefOfGroupOfIssues(CreateTypefOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            var idOfTypeOfGroupOfIssues = await _mediator.Send(new CreateTypeOfGroupOfIssuesCommand(request.Name, context.GetOrganizationId()));
            return new CreateTypefOfGroupOfIssuesResponse() {Id = idOfTypeOfGroupOfIssues};
        }

        public override async Task<DeleteTypeOfGroupOfIssuesResponse> DeleteTypeOfGroupOfIssues(DeleteTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ArchiveTypeOfGroupOfIssuesCommand(request.Id, context.GetOrganizationId()));
            return new DeleteTypeOfGroupOfIssuesResponse();
        }

        public override async Task<RenameTypeOfGroupOfIssuesResponse> RenameTypeOfGroupOfIssues(RenameTypeOfGroupOfIssuesRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameTypeOfGroupOfIssuesCommand(request.Id, context.GetOrganizationId(), request.NewName));
            return new RenameTypeOfGroupOfIssuesResponse();
        }

        private Protos.TypeOfGroupOfIssues MapToTypeOfGroupOfIssues(Domain.GroupsOfIssues.TypeOfGroupOfIssues type) =>
            new TypeOfGroupOfIssues() {Id = type.Id, Name = type.Name, OrganizationId = type.OrganizationId};
    }
}