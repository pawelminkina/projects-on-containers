using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.Status.ArchiveStatus
{
    public class ArchiveStatusCommandHandler : IRequestHandler<ArchiveStatusCommand>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveStatusCommandHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveStatusCommand request, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetStatusById(request.StatusId);
            ValidateStatusWithRequestedParameters(status, request);

            status.Archive();
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateStatusWithRequestedParameters(Domain.StatusesFlow.Status status, ArchiveStatusCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status with id: {request.StatusId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status with id: {request.StatusId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsArchived)
                throw new InvalidOperationException($"Status with id: {request.StatusId} is already archived");
        }

    }
}