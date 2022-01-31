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

namespace Issues.Application.CQRS.StatusFlow.Commands.AddStatusToFlow
{
    public class AddStatusToFlowCommand : IRequest<string>
    {
        public string StatusFlowId { get; }
        public string StatusName { get; }
        public string OrganizationId { get; }

        public AddStatusToFlowCommand(string statusFlowId, string statusName, string organizationId)
        {
            StatusFlowId = statusFlowId;
            StatusName = statusName;
            OrganizationId = organizationId;
        }
    }

    public class AddStatusToFlowCommandHandler : IRequestHandler<AddStatusToFlowCommand, string>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddStatusToFlowCommandHandler(IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(AddStatusToFlowCommand request, CancellationToken cancellationToken)
        {
            var statusFlow = await _statusFlowRepository.GetFlowById(request.StatusFlowId);
            ValidateStatusInFlowWithRequestedParameters(statusFlow,request);

            var status = statusFlow.AddNewStatusToFlow(request.StatusName);
            await _unitOfWork.CommitAsync(cancellationToken);

            return status.Id;
        }

        private void ValidateStatusInFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow statusFlow, AddStatusToFlowCommand request)
        {
            if (statusFlow is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.StatusFlowId);

            if (statusFlow.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.StatusFlowId, request.OrganizationId);
        }
    }
}
