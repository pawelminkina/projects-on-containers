using MediatR;

namespace Issues.Application.StatusInFlow.AddConnection
{
    public class AddConnectionCommand : IRequest
    {
        public AddConnectionCommand(string parentStatusId, string childStatusId, string flowId, string organizationId)
        {
            ParentStatusId = parentStatusId;
            ChildStatusId = childStatusId;
            FlowId = flowId;
            OrganizationId = organizationId;
        }

        public string ParentStatusId { get; }
        public string ChildStatusId { get; }
        public string FlowId { get; }
        public string OrganizationId { get; }
    }
}