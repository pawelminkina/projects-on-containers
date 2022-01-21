using System;
using System.Threading;
using System.Threading.Tasks;
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
                throw new InvalidOperationException($"Issue with id: {request.IssueId} was not found");

            if (issue.GroupOfIssue.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Issue with id: {request.IssueId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (issue.GroupOfIssue.IsDeleted)
                throw new InvalidOperationException($"Issue with id: {request.IssueId} is in deleted group with id: {issue.GroupOfIssue.Id}");
        }
    }
}