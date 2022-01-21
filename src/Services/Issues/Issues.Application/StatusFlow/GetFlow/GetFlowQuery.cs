using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlow
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
            if (flow is not null)
                ValidateFlowWithRequestedParameters(flow, request);

            return flow;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, GetFlowQuery request)
        {
            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Flow with id: {request.FlowId} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}