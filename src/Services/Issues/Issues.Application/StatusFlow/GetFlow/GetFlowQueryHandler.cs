using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlow
{
    public class GetFlowQueryHandler : IRequestHandler<GetFlowQuery, Domain.StatusesFlow.StatusFlow>
    {
        private readonly IStatusFlowRepository _statusRepository;

        public GetFlowQueryHandler(IStatusFlowRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<Domain.StatusesFlow.StatusFlow> Handle(GetFlowQuery request, CancellationToken cancellationToken)
        {
            var flow = await _statusRepository.GetFlowById(request.FlowId);
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