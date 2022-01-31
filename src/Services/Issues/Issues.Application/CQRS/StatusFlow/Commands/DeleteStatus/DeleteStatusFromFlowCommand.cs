using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.StatusFlow.Commands.DeleteStatus
{
    public class DeleteStatusFromFlowCommand : IRequest
    {
        public string StatusInFlowId { get; }
        public string OrganizationId { get; }

        public DeleteStatusFromFlowCommand(string statusInFlowId, string organizationId)
        {
            StatusInFlowId = statusInFlowId;
            OrganizationId = organizationId;
        }
    }

    public class DeleteStatusFromFlowCommandHandler : IRequestHandler<DeleteStatusFromFlowCommand>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStatusFromFlowCommandHandler(IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteStatusFromFlowCommand request, CancellationToken cancellationToken)
        {
            var statusToDelete = await _statusFlowRepository.GetStatusInFlowById(request.StatusInFlowId);
            ValidateStatusInFlowWithRequestedParameters(statusToDelete, request);

            var statusFlow = await _statusFlowRepository.GetFlowById(statusToDelete.StatusFlow.Id);
            statusFlow.DeleteStatusFromFlow(statusToDelete);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateStatusInFlowWithRequestedParameters(Domain.StatusesFlow.StatusInFlow statusInFlow, DeleteStatusFromFlowCommand request)
        {
            if (statusInFlow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.StatusInFlowId);

            if (statusInFlow.StatusFlow.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.StatusInFlowId, request.OrganizationId);
        }
    }
}
