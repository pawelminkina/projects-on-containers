using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.Status.CreateStatus
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, string>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStatusCommandHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            if (await StatusWithSameNameAlreadyExist(request.Name, request.OrganizationId))
                throw new InvalidOperationException($"Status with the same is already created, requested name: {request.Name}");

            var status = await _statusRepository.AddNewStatusAsync(request.Name, request.OrganizationId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return status.Id;
;       }
        private async Task<bool> StatusWithSameNameAlreadyExist(string name, string orgId) =>
            (await _statusRepository.GetStatusesForOrganization(orgId)).FirstOrDefault(s => s.Name == name) is not null;

    }
}