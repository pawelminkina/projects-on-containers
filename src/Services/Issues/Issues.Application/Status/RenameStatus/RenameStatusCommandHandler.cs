using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.Status.RenameStatus
{
    public class RenameStatusCommandHandler : IRequestHandler<RenameStatusCommand>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenameStatusCommandHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RenameStatusCommand request, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetStatusById(request.StatusId);
            ValidateStatusWithRequestedParameters(status,request);

            status.Rename(request.NewName);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateStatusWithRequestedParameters(Domain.StatusesFlow.Status status, RenameStatusCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status with id: {request.StatusId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status with id: {request.StatusId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsDeleted)
                throw new InvalidOperationException($"Status with id: {request.StatusId} is already deleted");
        }
    }
}