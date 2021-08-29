using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlowsForOrganization
{
    public class GetFlowsForOrganizationQueryHandler : IRequestHandler<GetFlowsForOrganizationQuery, IEnumerable<Domain.StatusesFlow.StatusFlow>>
    {
        private readonly IStatusRepository _statusRepository;

        public GetFlowsForOrganizationQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<Domain.StatusesFlow.StatusFlow>> Handle(GetFlowsForOrganizationQuery request, CancellationToken cancellationToken)
        {
            return await _statusRepository.GetFlowsByOrganization(request.OrganizationId);
        }
    }
}