using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.CQRS.GroupOfIssues.Commands.DeleteGroup
{
    public class DeleteGroupOfIssueCommand : IRequest
    {
        public string Id { get; }
        public string OrganizationId { get; }

        public DeleteGroupOfIssueCommand(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }
    }

    public class DeleteGroupOfIssueCommandHandler : IRequestHandler<DeleteGroupOfIssueCommand>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGroupOfIssueCommandHandler(IGroupOfIssuesRepository groupOfIssuesRepository, IUnitOfWork unitOfWork)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteGroupOfIssueCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.Id);
            ValidateTypeWithRequestedParameters(group, request);

            group.TypeOfGroup.DeleteGroupOfIssues(group.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, DeleteGroupOfIssueCommand request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdDoWasNotFound(request.Id);
            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.Id, request.OrganizationId);
        }
    }
}
