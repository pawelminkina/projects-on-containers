using MediatR;

namespace Issues.Application.Issues.UpdateIssueContent
{
    public class UpdateIssueContentCommand : IRequest
    {
        public UpdateIssueContentCommand(string issueId, string textContent, string organizationId)
        {
            IssueId = issueId;
            TextContent = textContent;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string TextContent { get; }
        public string OrganizationId { get; }
    }
}