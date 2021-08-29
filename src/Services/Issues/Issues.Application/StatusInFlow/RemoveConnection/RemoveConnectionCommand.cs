using MediatR;

namespace Issues.Application.StatusInFlow.RemoveConnection
{
    public class RemoveConnectionCommand : IRequest
    {
        public RemoveConnectionCommand(string flowId, string parentStatusId, string childStatusId, string organizationId)
        {
            FlowId = flowId;
            ParentStatusId = parentStatusId;
            ChildStatusId = childStatusId;
            OrganizationId = organizationId;
        }
        public string FlowId { get; }
        public string ParentStatusId { get; }
        public string ChildStatusId { get; }
        public string OrganizationId { get; }
    }
}