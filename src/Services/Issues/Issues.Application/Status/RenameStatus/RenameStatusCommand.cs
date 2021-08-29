using MediatR;

namespace Issues.Application.Status.RenameStatus
{
    public class RenameStatusCommand : IRequest
    {
        public RenameStatusCommand(string statusId, string newName, string organizationId)
        {
            StatusId = statusId;
            NewName = newName;
            OrganizationId = organizationId;
        }
        public string StatusId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }
    }
}