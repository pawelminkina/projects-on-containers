using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.StatusFlow.ArchiveFlow;
using Issues.Application.StatusFlow.CreateFlow;
using Issues.Application.StatusFlow.GetFlow;
using Issues.Application.StatusFlow.GetFlowsForOrganization;
using Issues.Application.StatusFlow.RenameFlow;
using Issues.Application.StatusInFlow.AddConnection;
using Issues.Application.StatusInFlow.AddStatusToFlow;
using Issues.Application.StatusInFlow.DeleteStatusFromFlow;
using Issues.Application.StatusInFlow.RemoveConnection;
using Issues.Domain.StatusesFlow;
using MediatR;
using Status = Grpc.Core.Status;
using StatusFlow = Issues.API.Protos.StatusFlow;
using StatusInFlow = Issues.API.Protos.StatusInFlow;

namespace Issues.API.GrpcServices
{
    public class GrpcStatusFlowService : Protos.StatusFlowService.StatusFlowServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcStatusFlowService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GetStatusFlowsResponse> GetStatusFlows(GetStatusFlowsRequest request, ServerCallContext context)
        {
            var flows = await _mediator.Send(new GetFlowsForOrganizationQuery(context.GetOrganizationId()));
            if (flows.Any())
            {
                var res = new GetStatusFlowsResponse();
                res.Flow.AddRange(flows.Select(MapToGrpcFlow));
                return res;
            }

            return new GetStatusFlowsResponse();
        }

        public override async Task<GetStatusFlowResponse> GetStatusFlow(GetStatusFlowRequest request, ServerCallContext context)
        {
            var flow = await _mediator.Send(new GetFlowQuery(request.Id, context.GetOrganizationId()));

            if (flow is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Flow with id: {request.Id} was not found"));

            return new GetStatusFlowResponse() {Flow = MapToGrpcFlow(flow)};
        }

        public override async Task<AddStatusFlowResponse> AddStatusFlow(AddStatusFlowRequest request, ServerCallContext context)
        {
            var res = await _mediator.Send(new CreateFlowCommand(request.Name, context.GetOrganizationId()));
            return new AddStatusFlowResponse() {Id = res};
        }

        public override async Task<DeleteStatusFlowsResponse> DeleteStatusFlow(DeleteStatusFlowsRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ArchiveFlowCommand(request.Id, context.GetOrganizationId()));
            return new DeleteStatusFlowsResponse();
        }

        public override async Task<RenameStatusFlowsResponse> RenameStatusFlow(RenameStatusFlowsRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RenameFlowCommand(request.Id, request.Name, context.GetOrganizationId()));
            return new RenameStatusFlowsResponse();
        }

        public override async Task<AddStatusToFlowResponse> AddStatusToFlow(AddStatusToFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new AddStatusToFlowCommand(request.StatusId, request.FlowId, context.GetOrganizationId()));
            return new AddStatusToFlowResponse();
        }

        public override async Task<DeleteStatusFromFlowResponse> DeleteStatusFromFlow(DeleteStatusFromFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteStatusFromFlowCommand(request.StatusId, request.FlowId, context.GetOrganizationId()));
            return new DeleteStatusFromFlowResponse();
        }

        public override async Task<AddConnectionToStatusInFlowResponse> AddConnectionToStatusInFlow(AddConnectionToStatusInFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new AddConnectionCommand(request.ParentStatusId, request.ConnectedStatusId, request.FlowId, context.GetOrganizationId()));
            return new AddConnectionToStatusInFlowResponse();
        }

        public override async Task<RemoveConnectionFromStatusInFlowResponse> RemoveConnectionFromStatusInFlow(RemoveConnectionFromStatusInFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RemoveConnectionCommand(request.FlowId, request.ParentStatusId, request.ConnectedStatusId, context.GetOrganizationId()));
            return new RemoveConnectionFromStatusInFlowResponse();
        }

        private StatusFlow MapToGrpcFlow(Domain.StatusesFlow.StatusFlow flow)
        {
            var res = new StatusFlow()
            {
                Id = flow.Id,
                Name = flow.Name
            };
            if (flow.StatusesInFlow.Any())
                res.Statuses.AddRange(flow.StatusesInFlow.Select(MapToGrpcStatusInFlow));
            return res;
        }

        private StatusInFlow MapToGrpcStatusInFlow(Domain.StatusesFlow.StatusInFlow statusInFlow)
        {
            var res = new StatusInFlow()
            {
                IndexInFLow = statusInFlow.IndexInFlow,
                ParentStatusId = statusInFlow.ParentStatusId
            };

            if (statusInFlow.ConnectedStatuses.Any())
                res.ConnectedStatuses.AddRange(statusInFlow.ConnectedStatuses.Select(d => new ConnectedStatuses()
                {
                    ConnectedStatusId = d.ConnectedStatus.Id,
                    ParentStatusId = d.ParentStatusInFlow.ParentStatus.Id,
                    DirectionOfStatus = MapToProtoDirection(d.Direction)
                }));
                
            return res;
        }

        private ConnectedStatuses.Types.Direction MapToProtoDirection(StatusInFlowDirection direction) =>
            direction == StatusInFlowDirection.In
                ? ConnectedStatuses.Types.Direction.In
                : ConnectedStatuses.Types.Direction.Out;
    }
}