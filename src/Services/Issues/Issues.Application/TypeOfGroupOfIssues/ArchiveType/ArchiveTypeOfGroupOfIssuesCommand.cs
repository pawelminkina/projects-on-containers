using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.ArchiveType
{
    public class ArchiveTypeOfGroupOfIssuesCommand : IRequest
    {
        public ArchiveTypeOfGroupOfIssuesCommand(string id, string organizationId, string typeOfGroupOfIssuesWhereGroupsWillBeMovedId)
        {
            Id = id;
            OrganizationId = organizationId;
            TypeOfGroupOfIssuesWhereGroupsWillBeMovedId = typeOfGroupOfIssuesWhereGroupsWillBeMovedId;
        }

        public string Id { get; }
        public string OrganizationId { get; }
        public string TypeOfGroupOfIssuesWhereGroupsWillBeMovedId { get; }
    }
}