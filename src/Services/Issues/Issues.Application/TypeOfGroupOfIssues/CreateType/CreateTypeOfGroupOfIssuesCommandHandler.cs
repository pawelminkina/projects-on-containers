using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.CreateType
{
    public class CreateTypeOfGroupOfIssuesCommandHandler : IRequestHandler<CreateTypeOfGroupOfIssuesCommand, string>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            if (await TypeWithSameNameAlreadyExist(request.Name, request.OrganizationId))
                throw new InvalidOperationException($"Type of group of issues with name: {request.Name} already exist");

            var type = new Domain.GroupsOfIssues.TypeOfGroupOfIssues(request.OrganizationId, request.Name);

            await _repository.AddNewTypeofGroupOfIssuesAsync(type);

            await _unitOfWork.CommitAsync(cancellationToken);

            return type.Id;
        }

        private async Task<bool> TypeWithSameNameAlreadyExist(string name, string orgId) =>
            (await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(orgId)).FirstOrDefault(s => s.Name == name) is not null;
    }
}