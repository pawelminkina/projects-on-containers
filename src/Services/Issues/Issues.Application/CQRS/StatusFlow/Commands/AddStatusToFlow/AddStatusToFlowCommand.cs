using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
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
                throw new InvalidOperationException($"Status flow with given id: {request.StatusFlowId} does not exist");

            if (statusFlow.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status flow with given id: {statusFlow.Id} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}
