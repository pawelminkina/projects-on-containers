﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.StatusFlow.Commands.RemoveConnection
{
    public class RemoveConnectionFromStatusInFlowCommand : IRequest
    {
        public string ParentStatusInFlowId { get; }
        public string ConnectedStatusInFlowId { get; }
        public string OrganizationId { get; }

        public RemoveConnectionFromStatusInFlowCommand(string parentStatusInFlowId, string connectedStatusInFlowId, string organizationId)
        {
            ParentStatusInFlowId = parentStatusInFlowId;
            ConnectedStatusInFlowId = connectedStatusInFlowId;
            OrganizationId = organizationId;
        }
    }

    public class RemoveConnectionFromStatusInFlowCommandHandler : IRequestHandler<RemoveConnectionFromStatusInFlowCommand>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveConnectionFromStatusInFlowCommandHandler(IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
        {
            _statusFlowRepository = statusFlowRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveConnectionFromStatusInFlowCommand request, CancellationToken cancellationToken)
        {
            var connectedStatusInFlow = await _statusFlowRepository.GetStatusInFlowById(request.ConnectedStatusInFlowId);
            if (connectedStatusInFlow is null)
                throw new InvalidOperationException($"Connected status in flow with given id: {request.ConnectedStatusInFlowId} does not exist");
            ValidateStatusInFlowWithRequestedParameters(connectedStatusInFlow, request);

            var parentStatusInFlow = await _statusFlowRepository.GetStatusInFlowById(request.ParentStatusInFlowId);
            if (parentStatusInFlow is null)
                throw new InvalidOperationException($"Connected status in flow with given id: {request.ParentStatusInFlowId} does not exist");
            ValidateStatusInFlowWithRequestedParameters(parentStatusInFlow, request);

            parentStatusInFlow.DeleteConnectedStatus(connectedStatusInFlow, StatusInFlowDirection.Out);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
        private void ValidateStatusInFlowWithRequestedParameters(Domain.StatusesFlow.StatusInFlow statusInFlow, RemoveConnectionFromStatusInFlowCommand request)
        {
            if (statusInFlow.StatusFlow.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status in flow with given id: {statusInFlow.Id} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}