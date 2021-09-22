using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusInFlow.AddConnection
{
    public class AddConnectionCommandHandler : IRequestHandler<AddConnectionCommand>
    {
        private readonly IStatusRepository _repository;
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddConnectionCommandHandler(IStatusRepository repository, IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddConnectionCommand request, CancellationToken cancellationToken)
        {
            var childStatus = await _repository.GetStatusById(request.ChildStatusId);
            ValidateStatusWithRequestedParameters(childStatus, request);

            var flow = await _statusFlowRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            var parentStatusInFlow = flow.StatusesInFlow.FirstOrDefault(d => d.ParentStatusId == request.ParentStatusId);
            if (parentStatusInFlow is null)
                throw new InvalidOperationException($"Requested parent status with id: {request.ParentStatusId} was not found with flow id: {request.FlowId}");

            parentStatusInFlow.AddConnectedStatus(childStatus);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, AddConnectionCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsArchived)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} is already archived");
        }

        private void ValidateStatusWithRequestedParameters(Domain.StatusesFlow.Status status, AddConnectionCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status with id: {request.ChildStatusId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status with id: {request.ChildStatusId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsDeleted)
                throw new InvalidOperationException($"Status with id: {request.ChildStatusId} is already deleted");
        }
    }
}