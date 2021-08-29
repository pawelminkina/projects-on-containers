using MediatR;

namespace Issues.Application.GroupOfIssues.RenameGroup
{
    public class RenameGroupOfIssuesCommand : IRequest
    {
        public RenameGroupOfIssuesCommand(string groupId, string newName, string organizationId)
        {
            GroupId = groupId;
            NewName = newName;
            OrganizationId = organizationId;
        }

        public string GroupId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }
    }
}