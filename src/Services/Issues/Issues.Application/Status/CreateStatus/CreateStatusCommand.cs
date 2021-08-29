using MediatR;

namespace Issues.Application.Status.CreateStatus
{
    public class CreateStatusCommand : IRequest<string>
    {
        public CreateStatusCommand(string name, string organizationId)
        {
            Name = name;
            OrganizationId = organizationId;
        }

        public string Name { get; }
        public string OrganizationId { get; }

    }
}