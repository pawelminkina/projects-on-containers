using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.Status.GetStatuses
{
    public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, IEnumerable<Domain.StatusesFlow.Status>>
    {
        private readonly IStatusRepository _statusRepository;

        public GetStatusesQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<Domain.StatusesFlow.Status>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _statusRepository.GetStatusesForOrganization(request.OrganizationId);
        }
    }
}