using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusInFlow.DeleteStatusFromFlow
{
    public class DeleteStatusFromFlowCommandHandler : IRequestHandler<DeleteStatusFromFlowCommand>
    {
        private readonly IStatusFlowRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStatusFromFlowCommandHandler(IStatusFlowRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteStatusFromFlowCommand request, CancellationToken cancellationToken)
        {
            var flow = await _statusRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            flow.DeleteStatusFromFlow(request.StatusId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, DeleteStatusFromFlowCommand request)
        {
            if (status is null)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was not found");

            if (status.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (status.IsArchived)
                throw new InvalidOperationException($"Status flow with id: {request.FlowId} is already archived");
        }
    }
}