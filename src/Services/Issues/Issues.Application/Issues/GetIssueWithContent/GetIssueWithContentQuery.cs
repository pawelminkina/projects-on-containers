using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.GetIssueWithContent
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
}