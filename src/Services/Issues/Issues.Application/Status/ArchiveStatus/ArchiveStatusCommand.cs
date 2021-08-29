using MediatR;

namespace Issues.Application.Status.ArchiveStatus
{
    public class ArchiveStatusCommand : IRequest
    {
        public string StatusId { get; }
        public string OrganizationId { get; }

        public ArchiveStatusCommand(string statusId, string organizationId)
        {
            StatusId = statusId;
            OrganizationId = organizationId;
        }
    }
}