using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.StatusFlow.CreateFlow
{
    public class CreateFlowCommandHandler : IRequestHandler<CreateFlowCommand, string>
    {
        private readonly IStatusFlowRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFlowCommandHandler(IStatusFlowRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateFlowCommand request, CancellationToken cancellationToken)
        {
            if (await FlowWithSameNameAlreadyExist(request.Name, request.OrganizationId))
                throw new InvalidOperationException($"Type of issues with name: {request.Name} already exist");

            var flow = await _statusRepository.AddNewStatusFlowAsync(request.Name, request.OrganizationId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return flow.Id;
        }

        private async Task<bool> FlowWithSameNameAlreadyExist(string name, string orgId) =>
            (await _statusRepository.GetFlowsByOrganizationAsync(orgId)).FirstOrDefault(s => s.Name == name) is not null;

    }
}