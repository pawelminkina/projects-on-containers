using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.GroupOfIssues.RenameGroup
{
    public class RenameGroupOfIssuesCommandHandler : IRequestHandler<RenameGroupOfIssuesCommand>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IGroupOfIssuesRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenameGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IGroupOfIssuesRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RenameGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupOfIssuesByIdAsync(request.GroupId);
            ValidateTypeWithRequestedParameters(group, request);

            if (await GroupWithSameNameAlreadyExist(request.NewName, request.OrganizationId))
                throw new InvalidOperationException($"Group of issues with name: {request.NewName} already exist");

            group.Rename(request.NewName);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task<bool> GroupWithSameNameAlreadyExist(string name, string organizationId) =>
            (await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(organizationId)).FirstOrDefault(s => s.Name == name) is not null;

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, RenameGroupOfIssuesCommand request)
        {
            if (group is null)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} was not found");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (group.IsArchived)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} is already archived");
        }
    }
}