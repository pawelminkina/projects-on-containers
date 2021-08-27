using MediatR;

namespace Issues.Application.TypeOfIssues.ArchiveType
{
    public class ArchiveTypeOfIssuesCommand : IRequest
    {
        public ArchiveTypeOfIssuesCommand(string typeOfIssuesId, string organizationId)
        {
            TypeOfIssuesId = typeOfIssuesId;
            OrganizationId = organizationId;
        }

        public string TypeOfIssuesId { get; }
        public string OrganizationId { get; }
    }
}