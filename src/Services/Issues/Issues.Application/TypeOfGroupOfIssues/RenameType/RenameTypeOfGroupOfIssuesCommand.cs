using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.RenameType
{
    public class RenameTypeOfGroupOfIssuesCommand : IRequest
    {
        public RenameTypeOfGroupOfIssuesCommand(string id, string organizationId, string newName)
        {
            Id = id;
            OrganizationId = organizationId;
            NewName = newName;
        }

        public string Id { get; }
        public string OrganizationId { get; }
        public string NewName { get; }
    }
}