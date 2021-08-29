using System.Collections.Generic;
using MediatR;

namespace Issues.Application.StatusFlow.GetFlowsForOrganization
{
    public class GetFlowsForOrganizationQuery : IRequest<IEnumerable<Domain.StatusesFlow.StatusFlow>>
    {
        public GetFlowsForOrganizationQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}