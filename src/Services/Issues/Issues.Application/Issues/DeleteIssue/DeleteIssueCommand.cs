using MediatR;

namespace Issues.Application.Issues.DeleteIssue
{
    public class DeleteIssueCommand : IRequest
    {
        public DeleteIssueCommand(string issueId, string organizationId)
        {
            IssueId = issueId;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string OrganizationId { get; }
    }
}