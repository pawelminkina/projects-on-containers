using System;
using System.Formats.Asn1;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.GroupOfIssues.CreateGroup
{
    public class CreateGroupOfIssuesCommandHandler : IRequestHandler<CreateGroupOfIssuesCommand, string>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.TypeOfGroupId);
            ValidateTypeWithRequestedParameters(type, request);

            if (await GroupWithSameNameAlreadyExist(request.Name, request.OrganizationId))
                throw new InvalidOperationException($"Group of issues with name: {request.Name} already exist");

            var group = type.AddNewGroupOfIssues(request.Name);
            
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return group.Id;
        }

        private async Task<bool> GroupWithSameNameAlreadyExist(string name, string organizationId) =>
        (await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(organizationId)).FirstOrDefault(s => s.Name == name) is not null;

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, CreateGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of group of issue with id: {request.TypeOfGroupId} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issue with id: {request.TypeOfGroupId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of group of issue with id: {request.TypeOfGroupId} is already archived");
        }
    }
}