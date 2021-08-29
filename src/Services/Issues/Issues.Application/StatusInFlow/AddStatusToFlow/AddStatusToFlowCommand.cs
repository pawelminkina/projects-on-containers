using MediatR;

namespace Issues.Application.StatusInFlow.AddStatusToFlow
{
    public class AddStatusToFlowCommand : IRequest
    {
        public AddStatusToFlowCommand(string statusId, string flowId, string organizationId)
        {
            StatusId = statusId;
            FlowId = flowId;
            OrganizationId = organizationId;
        }

        public string StatusId { get; }
        public string FlowId { get; }
        public string OrganizationId { get; }
    }
}