using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.CQRS.Issues.Queries.GetIssueWithContent
{
    public class GetIssueWithContentQuery : IRequest<Issue>
    {
        public GetIssueWithContentQuery(string issueId, string organizationId)
        {
            IssueId = issueId;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string OrganizationId { get; }
    }

    public class GetIssueWithContentQueryHandler : IRequestHandler<GetIssueWithContentQuery, Issue>
    {
        public string IssuesNotAvailableForDeletedGroupErrorMessage(string groupId, string issueId) => $"Issue with id: {issueId} is in deleted group with id: {groupId}";

        private readonly IIssueRepository _issueRepository;

        public GetIssueWithContentQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
        public async Task<Issue> Handle(GetIssueWithContentQuery request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
            ValidateIssueWithRequestedParameters(issue, request);

            return issue;
        }

        private void ValidateIssueWithRequestedParameters(Domain.Issues.Issue issue, GetIssueWithContentQuery request)
        {
            if (issue is null)
                throw NotFoundException.RequestedResourceWithIdDoWasNotFound(request.IssueId);

            if (issue.GroupOfIssue.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.IssueId, request.OrganizationId);

            if (issue.GroupOfIssue.IsDeleted)
                throw new NotFoundException(IssuesNotAvailableForDeletedGroupErrorMessage(issue.GroupOfIssue.Id, request.IssueId));
        }
    }
}