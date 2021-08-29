using MediatR;

namespace Issues.Application.StatusFlow.GetFlow
{
    public class GetFlowQuery : IRequest<Domain.StatusesFlow.StatusFlow>
    {
        public GetFlowQuery(string flowId, string organizationId)
        {
            FlowId = flowId;
            OrganizationId = organizationId;
        }

        public string FlowId { get; }
        public string OrganizationId { get; }
    }
}