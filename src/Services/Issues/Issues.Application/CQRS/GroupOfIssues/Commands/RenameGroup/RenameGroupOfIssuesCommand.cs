using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Issues.Application.CQRS.GroupOfIssues.Commands.RenameGroup
{
    public class RenameGroupOfIssuesCommand : IRequest
    {
        public RenameGroupOfIssuesCommand(string groupId, string newName, string organizationId)
        {
            GroupId = groupId;
            NewName = newName;
            OrganizationId = organizationId;
        }

        public string GroupId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }
    }

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

            group.Rename(request.NewName);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }


        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, RenameGroupOfIssuesCommand request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.GroupId);
            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.GroupId, request.OrganizationId);
        }
    }
}