using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.StatusFlow.Queries.GetFlow
{
    public class GetFlowQuery : IRequest<Domain.StatusesFlow.StatusFlow>
    {
        public GetFlowQuery(string flowId, string organizationId)
        {
            FlowId = flowId;
            OrganizationId = organizationId;
        }

        public string FlowId { get; }
        public string OrganizationId { get; }
    }
    public class GetFlowQueryHandler : IRequestHandler<GetFlowQuery, Domain.StatusesFlow.StatusFlow>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;

        public GetFlowQueryHandler(IStatusFlowRepository statusFlowRepository)
        {
            _statusFlowRepository = statusFlowRepository;
        }
        public async Task<Domain.StatusesFlow.StatusFlow> Handle(GetFlowQuery request, CancellationToken cancellationToken)
        {
            var flow = await _statusFlowRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            return flow;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow flow, GetFlowQuery request)
        {
            if (flow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.FlowId);

            if (flow.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.FlowId, request.OrganizationId);
        }
    }
}