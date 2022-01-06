using MediatR;

namespace Issues.Application.StatusInFlow.RemoveConnection
{
    public class RemoveConnectionCommand : IRequest
    {
        public RemoveConnectionCommand(string flowId, string parentStatusId, string connectedStatusId, string organizationId)
        {
            FlowId = flowId;
            ParentStatusId = parentStatusId;
            ConnectedStatusId = connectedStatusId;
            OrganizationId = organizationId;
        }
        public string FlowId { get; }
        public string ParentStatusId { get; }
        public string ConnectedStatusId { get; }
        public string OrganizationId { get; }
    }
}