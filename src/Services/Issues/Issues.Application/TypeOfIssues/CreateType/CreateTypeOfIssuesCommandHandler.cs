using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.CreateType
{
    public class CreateTypeOfIssuesCommandHandler : IRequestHandler<CreateTypeOfIssuesCommand, string>
    {
        private readonly ITypeOfIssueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTypeOfIssuesCommandHandler(ITypeOfIssueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateTypeOfIssuesCommand request, CancellationToken cancellationToken)
        {
            if (await TypeWithSameNameAlreadyExist(request.Name, request.OrganizationId))
                throw new InvalidOperationException($"Type of issues with name: {request.Name} already exist");

            var typeOfIssue = new TypeOfIssue(request.OrganizationId, request.Name);

            await _repository.AddNewTypeOfIssueAsync(typeOfIssue);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return typeOfIssue.Id;
        }

        private async Task<bool> TypeWithSameNameAlreadyExist(string name, string orgId) =>
            (await _repository.GetTypeOfIssuesForOrganizationAsync(orgId)).FirstOrDefault(s => s.Name == name) is not null;
    }
}