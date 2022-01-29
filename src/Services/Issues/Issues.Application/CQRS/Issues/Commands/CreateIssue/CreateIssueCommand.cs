using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.CQRS.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<string>
    {
        public CreateIssueCommand(string name, string groupId, string textContent, string userId, string organizationId)
        {
            Name = name;
            GroupId = groupId;
            TextContent = textContent;
            UserId = userId;
            OrganizationId = organizationId;
        }
        public string Name { get; }
        public string GroupId { get; }
        public string TextContent { get; }
        public string UserId { get; }
        public string OrganizationId { get; }
    }
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, string>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIssueCommandHandler(IGroupOfIssuesRepository groupOfIssuesRepository, IUnitOfWork unitOfWork)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.GroupId);
            ValidateGroupWithRequestedParameters(group, request);

            var issue = group.AddIssue(request.Name, request.UserId, request.TextContent);
            await _unitOfWork.CommitAsync(cancellationToken);

            return issue.Id;
        }

        private void ValidateGroupWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, CreateIssueCommand request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdDoWasNotFound(request.GroupId);

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.GroupId, request.OrganizationId);
        }
    }
}