using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.Status.CreateStatus;
using Issues.Application.Status.GetStatus;
using Issues.Application.Status.GetStatuses;
using Issues.Application.Status.RenameStatus;
using MediatR;
using Status = Issues.API.Protos.Status;

namespace Issues.API.GrpcServices
{
    public class GrpcStatusService : Protos.StatusService.StatusServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcStatusService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GetStatusesResponse> GetStatuses(GetStatusesRequest request, ServerCallContext context)
        {
            var statuses = await _mediator.Send(new GetStatusesQuery(context.GetOrganizationId()));

            if (statuses.Any())
            {
                var res = new GetStatusesResponse();
                res.Statuses.AddRange(statuses.Select(MapToGrpcStatus));
                return res;
            }

            return new GetStatusesResponse();
        }
        public override async Task<GetStatusResponse> GetStatus(GetStatusRequest request, ServerCallContext context)
        {
            var status = await _mediator.Send(new GetStatusQuery(request.Id, context.GetOrganizationId()));
            if (status is null)
                throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, $"Status with id: {request.Id} was not found"));
            
            return new GetStatusResponse() {Status = MapToGrpcStatus(status)};
        }

        public override async Task<CreateStatusResponse> CreateStatus(CreateStatusRequest request, ServerCallContext context)
        {
            var id = await _mediator.Send(new CreateStatusCommand(request.Name, context.GetOrganizationId()));
            return new CreateStatusResponse() { Id = id };
        }

        public override async Task<RenameStatusResponse> RenameStatus(RenameStatusRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameStatusCommand(request.Id, request.Name, context.GetOrganizationId()));
            return new RenameStatusResponse();
        }

        private Status MapToGrpcStatus(Domain.StatusesFlow.Status status) => new Status()
        {
            Id = status.Id,
            Name = status.Name
        };
    }
}