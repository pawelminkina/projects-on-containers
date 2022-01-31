using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using FluentValidation.Validators;
using Issues.Application.Common.Exceptions;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.StatusFlow.Commands.AddConnection
{
    public class AddConnectionToStatusInFlowCommand : IRequest
    {
        public string ParentStatusInFlowId { get; }
        public string ConnectedStatusInFlowId { get; }
        public string OrganizationId { get; }

        public AddConnectionToStatusInFlowCommand(string parentStatusInFlowId, string connectedStatusInFlowId, string organizationId)
        {
            ParentStatusInFlowId = parentStatusInFlowId;
            ConnectedStatusInFlowId = connectedStatusInFlowId;
            OrganizationId = organizationId;
        }
    }

    public class AddConnectionToStatusInFlowCommandHandler : IRequestHandler<AddConnectionToStatusInFlowCommand>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddConnectionToStatusInFlowCommandHandler(IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddConnectionToStatusInFlowCommand request, CancellationToken cancellationToken)
        {
            var connectedStatusInFlow = await _statusFlowRepository.GetStatusInFlowById(request.ConnectedStatusInFlowId);

            if (connectedStatusInFlow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.ConnectedStatusInFlowId);

            ValidateStatusInFlowWithRequestedParameters(connectedStatusInFlow, request);

            var parentStatusInFlow = await _statusFlowRepository.GetStatusInFlowById(request.ParentStatusInFlowId);
            
            if (parentStatusInFlow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.ParentStatusInFlowId);

            ValidateStatusInFlowWithRequestedParameters(parentStatusInFlow, request);
            
            parentStatusInFlow.AddConnectedStatus(connectedStatusInFlow);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateStatusInFlowWithRequestedParameters(Domain.StatusesFlow.StatusInFlow statusInFlow, AddConnectionToStatusInFlowCommand request)
        {
            if (statusInFlow.StatusFlow.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(statusInFlow.Id, request.OrganizationId);
        }
    }
}
