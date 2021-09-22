using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusInFlow.RemoveConnection
{
    public class RemoveConnectionCommandHandler : IRequestHandler<RemoveConnectionCommand>
    {
        private readonly IStatusFlowRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveConnectionCommandHandler(IStatusFlowRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveConnectionCommand request, CancellationToken cancellationToken)
        {
            var flow = await _statusRepository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            var statusInFlow = flow.StatusesInFlow.FirstOrDefault(d => d.ParentStatusId == request.ParentStatusId);

            if (statusInFlow is null)
                throw new InvalidOperationException(
                    $"Status with id: {request.ParentStatusId} was not found in flow with id: {request.FlowId}");

            var connection = statusInFlow.ConnectedStatuses.FirstOrDefault(d => d.ConnectedWithParentId == request.ChildStatusId);

            if (connection is null)
                throw new InvalidOperationException(
                    $"Connection in flow with id: {request.FlowId} was not found for parentId: {request.ParentStatusId} and child id: {request.ChildStatusId}");

            statusInFlow.DeleteConnectedStatus(connection.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, RemoveConnectionCommand request)
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