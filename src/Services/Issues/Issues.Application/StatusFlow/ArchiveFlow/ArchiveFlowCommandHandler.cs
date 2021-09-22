using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.ArchiveFlow
{
    public class ArchiveFlowCommandHandler : IRequestHandler<ArchiveFlowCommand>
    {
        private readonly IStatusFlowRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveFlowCommandHandler(IStatusFlowRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveFlowCommand request, CancellationToken cancellationToken)
        {
            var flow = await _repository.GetFlowById(request.FlowId);
            ValidateFlowWithRequestedParameters(flow, request);

            flow.Archive();
            await _unitOfWork.CommitAsync(cancellationToken);

            return  Unit.Value;
        }

        private void ValidateFlowWithRequestedParameters(Domain.StatusesFlow.StatusFlow status, ArchiveFlowCommand request)
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