using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.TypeOfGroupOfIssues.GetTypes;
using Issues.Application.TypeOfIssues.ArchiveType;
using Issues.Application.TypeOfIssues.CreateType;
using Issues.Application.TypeOfIssues.GetType;
using Issues.Application.TypeOfIssues.GetTypes;
using Issues.Application.TypeOfIssues.RenameType;
using MediatR;
using Status = Grpc.Core.Status;

namespace Issues.API.GrpcServices
{
    public class GrpcTypeOfIssueService : Protos.TypeOfIssueService.TypeOfIssueServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcTypeOfIssueService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GetTypesOfIssuesResponse> GetTypesOfIssues(GetTypesOfIssuesRequest request, ServerCallContext context)
        {
            var types = await _mediator.Send(new GetTypesOfIssuesQuery(context.GetOrganizationId()));
            if (types.Any())
            {
                var res = new GetTypesOfIssuesResponse();
                res.Types_.AddRange(types.Select(MapToGrpcTypeOfIssue));
                return res;
            }

            return new GetTypesOfIssuesResponse();
        }

        public override async Task<GetTypeOfIssueResponse> GetTypeOfIssue(GetTypeOfIssueRequest request, ServerCallContext context)
        {
            var type = await _mediator.Send(new GetTypeOfIssueQuery(request.Id, context.GetOrganizationId()));
            if (type is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Type of issues with id: {request.Id} was not found"));

            return new GetTypeOfIssueResponse() {Type = MapToGrpcTypeOfIssue(type)};
        }

        public override async Task<CreateTypeOfIssueResponse> CreateTypeOfIssue(CreateTypeOfIssueRequest request, ServerCallContext context)
        {
            var id = await _mediator.Send(new CreateTypeOfIssuesCommand(request.Name, context.GetOrganizationId()));
            return new CreateTypeOfIssueResponse() {Id = id};
        }

        public override async Task<ArchiveTypeOfIssueResponse> ArchiveTypeOfIssue(ArchiveTypeOfIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ArchiveTypeOfIssuesCommand(request.Id, context.GetOrganizationId()));
            return new ArchiveTypeOfIssueResponse();
        }
        public override async Task<RenameTypeOfIssueResponse> RenameTypeOfIssue(RenameTypeOfIssueRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameTypeOfIssuesCommand(request.Id, request.Name, context.GetOrganizationId()));
            return new RenameTypeOfIssueResponse();
        }

        private TypeOfIssue MapToGrpcTypeOfIssue(Domain.TypesOfIssues.TypeOfIssue type) =>
            new TypeOfIssue() {Id = type.Id, Name = type.Name};
    }
}