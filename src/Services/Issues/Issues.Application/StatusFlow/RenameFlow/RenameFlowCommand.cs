using MediatR;

namespace Issues.Application.StatusFlow.RenameFlow
{
    public class RenameFlowCommand : IRequest
    {
        public RenameFlowCommand(string flowId, string newName, string organizationId)
        {
            FlowId = flowId;
            NewName = newName;
            OrganizationId = organizationId;
        }

        public string FlowId { get; }
        public string NewName { get; }
        public string OrganizationId { get; }
    }
}