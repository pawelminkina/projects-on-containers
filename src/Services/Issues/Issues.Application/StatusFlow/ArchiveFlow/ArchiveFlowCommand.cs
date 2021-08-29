using MediatR;

namespace Issues.Application.StatusFlow.ArchiveFlow
{
    public class ArchiveFlowCommand : IRequest
    {
        public ArchiveFlowCommand(string flowId, string organizationId)
        {
            FlowId = flowId;
            OrganizationId = organizationId;
        }

        public string FlowId { get; }
        public string OrganizationId { get; }

    }
}