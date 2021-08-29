using MediatR;

namespace Issues.Application.TypeOfIssues.RenameType
{
    public class RenameTypeOfIssuesCommand : IRequest
    {
        public RenameTypeOfIssuesCommand(string typeOfIssueId, string newName, string organizationId)
        {
            TypeOfIssueId = typeOfIssueId;
            NewName = newName;
            OrganizationId = organizationId;
        }

        public string TypeOfIssueId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }

    }
}