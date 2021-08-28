using MediatR;

namespace Issues.Application.GroupOfIssues.ArchiveGroup
{
    public class ArchiveGroupOfIssuesCommand : IRequest
    {

        public ArchiveGroupOfIssuesCommand(string groupOfIssuesId, string organizationId)
        {
            GroupOfIssuesId = groupOfIssuesId;
            OrganizationId = organizationId;
        }

        public string GroupOfIssuesId { get; }
        public string OrganizationId { get; }
    }
}