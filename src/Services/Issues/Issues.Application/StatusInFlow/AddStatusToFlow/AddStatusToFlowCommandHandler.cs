using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusInFlow.AddStatusToFlow
{
    public class AddStatusToFlowCommandHandler : IRequestHandler<AddStatusToFlowCommand>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddStatusToFlowCommandHandler(IStatusRepository statusRepository, IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddStatusToFlowCommand request, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetStatusById(request.StatusId);
            ValidateStatusWithRequestedParameters(status,request);

            var flow = await _statusFlowRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow,request);

            flow.AddNewStatusToFlow(status);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, AddStatusToFlowCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsArchived)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} is already archived");
        }

        private void ValidateStatusWithRequestedParameters(Domain.StatusesFlow.Status status, AddStatusToFlowCommand request)
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