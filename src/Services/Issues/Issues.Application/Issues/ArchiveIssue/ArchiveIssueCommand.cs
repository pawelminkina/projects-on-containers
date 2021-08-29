using MediatR;

namespace Issues.Application.Issues.ArchiveIssue
{
    public class ArchiveIssueCommand : IRequest
    {
        public ArchiveIssueCommand(string issueId, string organizationId)
        {
            IssueId = issueId;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string OrganizationId { get; }
    }
}
