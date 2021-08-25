using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.ArchiveType
{
    public class ArchiveTypeOfGroupOfIssuesCommand : IRequest
    {
        public ArchiveTypeOfGroupOfIssuesCommand(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }

    }
}