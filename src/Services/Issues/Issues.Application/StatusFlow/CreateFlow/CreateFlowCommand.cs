using MediatR;

namespace Issues.Application.StatusFlow.CreateFlow
{
    public class CreateFlowCommand : IRequest<string>
    {

        public CreateFlowCommand(string name, string organizationId)
        {
            Name = name;
            OrganizationId = organizationId;
        }

        public string Name { get; }
        public string OrganizationId { get; }
    }
}