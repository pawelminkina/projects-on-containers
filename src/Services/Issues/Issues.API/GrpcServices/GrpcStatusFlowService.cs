using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.StatusFlow.AddConnection;
using Issues.Application.StatusFlow.AddStatusToFlow;
using Issues.Application.StatusFlow.DeleteStatus;
using Issues.Application.StatusFlow.GetFlow;
using Issues.Application.StatusFlow.GetFlowsForOrganization;
using Issues.Application.StatusFlow.RemoveConnection;
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

        public override async Task<GetStatusFlowForGroupOfIssuesResponse> GetStatusFlowForGroupOfIssues(GetStatusFlowForGroupOfIssuesRequest request, ServerCallContext context)
        {
            return await base.GetStatusFlowForGroupOfIssues(request, context);
        }

        public override async Task<AddStatusToFlowResponse> AddStatusToFlow(AddStatusToFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new AddStatusToFlowCommand(request.FlowId, request.StatusName, context.GetOrganizationId()));
            return new AddStatusToFlowResponse();
        }

        public override async Task<DeleteStatusFromFlowResponse> DeleteStatusFromFlow(DeleteStatusFromFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteStatusFromFlowCommand(request.StatusInFlowId, context.GetOrganizationId()));
            return new DeleteStatusFromFlowResponse();
        }

        public override async Task<AddConnectionToStatusInFlowResponse> AddConnectionToStatusInFlow(AddConnectionToStatusInFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new AddConnectionToStatusInFlowCommand(request.ParentStatusinFlowId, request.ConnectedStatusInFlowId, context.GetOrganizationId()));
            return new AddConnectionToStatusInFlowResponse();
        }

        public override async Task<RemoveConnectionFromStatusInFlowResponse> RemoveConnectionFromStatusInFlow(RemoveConnectionFromStatusInFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new RemoveConnectionFromStatusInFlowCommand(request.ParentStatusinFlowId, request.ConnectedStatusInFlowId, context.GetOrganizationId()));
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
                Name = statusInFlow.Name,
                Id = statusInFlow.Id
            };

            if (statusInFlow.ConnectedStatuses.Any())
                res.ConnectedStatuses.AddRange(statusInFlow.ConnectedStatuses.Select(d => new ConnectedStatuses()
                {
                    ConnectedStatusInFlowId = d.ConnectedStatusInFlow.Id,
                    ParentStatusInFlowIdId = d.ParentStatusInFlow.Id,
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