using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlowsForOrganization
{
    public class GetFlowsForOrganizationQuery : IRequest<IEnumerable<Domain.StatusesFlow.StatusFlow>>
    {
        public GetFlowsForOrganizationQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
    public class GetFlowsForOrganizationQueryHandler : IRequestHandler<GetFlowsForOrganizationQuery, IEnumerable<Domain.StatusesFlow.StatusFlow>>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;

        public GetFlowsForOrganizationQueryHandler(IStatusFlowRepository statusFlowRepository)
        {
            _statusFlowRepository = statusFlowRepository;
        }
        public async Task<IEnumerable<Domain.StatusesFlow.StatusFlow>> Handle(GetFlowsForOrganizationQuery request, CancellationToken cancellationToken)
        {
            var flows = await _statusFlowRepository.GetFlowsByOrganizationAsync(request.OrganizationId);
            return flows.Where(s => !s.IsDeleted);
        }
    }
}