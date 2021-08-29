using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlow
{
    public class GetFlowQueryHandler : IRequestHandler<GetFlowQuery, Domain.StatusesFlow.StatusFlow>
    {
        private readonly IStatusRepository _statusRepository;

        public GetFlowQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<Domain.StatusesFlow.StatusFlow> Handle(GetFlowQuery request, CancellationToken cancellationToken)
        {
            var flow = await _statusRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            return flow;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, GetFlowQuery request)
        {
            if (status is null)
                throw new InvalidOperationException($"Flow with id: {request.FlowId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Flow with id: {request.FlowId} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}