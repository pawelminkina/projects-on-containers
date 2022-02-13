using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Issues.API.Extensions;
using Issues.API.Protos;
using Issues.Application.CQRS.StatusFlow.Commands.AddConnection;
using Issues.Application.CQRS.StatusFlow.Commands.AddStatusToFlow;
using Issues.Application.CQRS.StatusFlow.Commands.ChangeDefaultStatus;
using Issues.Application.CQRS.StatusFlow.Commands.DeleteStatus;
using Issues.Application.CQRS.StatusFlow.Commands.RemoveConnection;
using Issues.Application.CQRS.StatusFlow.Queries.GetFlow;
using Issues.Application.CQRS.StatusFlow.Queries.GetFlowsForOrganization;
using Issues.Application.CQRS.StatusFlow.Queries.GetStatusFlowForGroup;
using Issues.Domain.StatusesFlow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Status = Grpc.Core.Status;
using StatusFlow = Issues.API.Protos.StatusFlow;
using StatusInFlow = Issues.API.Protos.StatusInFlow;

namespace Issues.API.GrpcServices
{
    [Authorize]
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
                res.Flows.AddRange(flows.Select(MapToGrpcFlow));
                return res;
            }

            return new GetStatusFlowsResponse();
        }

        public override async Task<GetStatusFlowResponse> GetStatusFlow(GetStatusFlowRequest request, ServerCallContext context)
        {
            var flow = await _mediator.Send(new GetFlowQuery(request.Id, context.GetOrganizationId()));
            return new GetStatusFlowResponse() {Flow = MapToGrpcFlow(flow)};
        }

        public override async Task<GetStatusFlowForGroupOfIssuesResponse> GetStatusFlowForGroupOfIssues(GetStatusFlowForGroupOfIssuesRequest request, ServerCallContext context)
        {
            var flow = await _mediator.Send(new GetStatusFlowForGroupOfIssuesQuery(request.GroupOfIssuesId, context.GetOrganizationId()));
            return new GetStatusFlowForGroupOfIssuesResponse() { Flow = MapToGrpcFlow(flow) };
        }

        public override async Task<AddStatusToFlowResponse> AddStatusToFlow(AddStatusToFlowRequest request, ServerCallContext context)
        {
            var statusId = await _mediator.Send(new AddStatusToFlowCommand(request.FlowId, request.StatusName, context.GetOrganizationId()));
            return new AddStatusToFlowResponse() {StatusId = statusId};
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

        public override async Task<ChangeDefaultStatusInFlowResponse> ChangeDefaultStatusInFlow(ChangeDefaultStatusInFlowRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ChangeDefaultStatusInFlowCommand(request.NewDefaultStatusInFlowId, context.GetOrganizationId()));
            return new ChangeDefaultStatusInFlowResponse();
        }


        private StatusFlow MapToGrpcFlow(Domain.StatusesFlow.StatusFlow flow)
        {
            var res = new StatusFlow()
            {
                Id = flow.Id,
                Name = flow.Name,
                IsDefault = flow.IsDefault,
                IsDeleted = flow.IsDeleted
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
                Id = statusInFlow.Id,
                IsDefault = statusInFlow.IsDefault
            };

            if (statusInFlow.ConnectedStatuses.Any())
                res.ConnectedStatusesId.AddRange(statusInFlow.ConnectedStatuses.Select(s=>s.ConnectedStatusInFlow.Id));
                
            return res;
        }
    }
}