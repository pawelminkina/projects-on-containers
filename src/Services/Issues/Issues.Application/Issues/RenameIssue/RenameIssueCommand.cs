using MediatR;

namespace Issues.Application.Issues.RenameIssue
{
    public class RenameIssueCommand : IRequest
    {
        public RenameIssueCommand(string issueId, string newName, string organizationId)
        {
            IssueId = issueId;
            NewName = newName;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }
    }
}