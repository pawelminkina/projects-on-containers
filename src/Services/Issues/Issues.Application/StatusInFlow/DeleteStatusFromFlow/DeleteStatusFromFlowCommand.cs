using MediatR;

namespace Issues.Application.StatusInFlow.DeleteStatusFromFlow
{
    public class DeleteStatusFromFlowCommand : IRequest
    {
        public DeleteStatusFromFlowCommand(string statusId, string flowId, string organizationId)
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