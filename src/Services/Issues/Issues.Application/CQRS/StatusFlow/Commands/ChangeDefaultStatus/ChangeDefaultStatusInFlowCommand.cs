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

namespace Issues.Application.CQRS.StatusFlow.Commands.ChangeDefaultStatus
{
    public class ChangeDefaultStatusInFlowCommand : IRequest
    {
        public string NewDefaultStatusId { get; }
        public string OrganizationId { get; }

        public ChangeDefaultStatusInFlowCommand(string newDefaultStatusId, string organizationId)
        {
            NewDefaultStatusId = newDefaultStatusId;
            OrganizationId = organizationId;
        }
    }

    public class ChangeDefaultStatusInFlowCommandHandler : IRequestHandler<ChangeDefaultStatusInFlowCommand>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDefaultStatusInFlowCommandHandler(IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangeDefaultStatusInFlowCommand request, CancellationToken cancellationToken)
        {
            var statusInFLow = await _statusFlowRepository.GetStatusInFlowById(request.NewDefaultStatusId);
            ValidateStatusInFlowWithRequestedParameters(statusInFLow, request);

            statusInFLow.StatusFlow.ChangeDefaultStatusInFlow(statusInFLow);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateStatusInFlowWithRequestedParameters(Domain.StatusesFlow.StatusInFlow statusInFlow, ChangeDefaultStatusInFlowCommand request)
        {
            if (statusInFlow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.NewDefaultStatusId);

            if (statusInFlow.StatusFlow.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.NewDefaultStatusId, request.OrganizationId);
        }
    }
}
