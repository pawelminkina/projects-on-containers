using MediatR;

namespace Issues.Application.TypeOfIssues.RenameType
{
    public class RenameTypeOfIssuesCommand : IRequest
    {
        public RenameTypeOfIssuesCommand(string typeIfIssueId, string newName, string organizationId)
        {
            TypeIfIssueId = typeIfIssueId;
            NewName = newName;
            OrganizationId = organizationId;
        }

        public string TypeIfIssueId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }

    }
}