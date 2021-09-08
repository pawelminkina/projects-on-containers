using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.Status.GetStatus
{
    public class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, Domain.StatusesFlow.Status>
    {
        private readonly IStatusRepository _statusRepository;

        public GetStatusQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<Domain.StatusesFlow.Status> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetStatusById(request.StatusId);
            if (status is not null)
                ValidateStatusWithRequestedParameters(status, request);

            return status;
        }

        private void ValidateStatusWithRequestedParameters(Domain.StatusesFlow.Status status, GetStatusQuery request)
        {
            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status with id: {request.StatusId} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}