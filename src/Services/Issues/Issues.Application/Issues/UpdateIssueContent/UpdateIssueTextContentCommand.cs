using MediatR;

namespace Issues.Application.Issues.UpdateIssueContent
{
    public class UpdateIssueTextContentCommand : IRequest
    {
        public UpdateIssueTextContentCommand(string issueId, string textContent, string organizationId)
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