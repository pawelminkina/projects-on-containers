using System.Collections.Generic;
using MediatR;

namespace Issues.Application.Status.GetStatuses
{
    public class GetStatusesQuery : IRequest<IEnumerable<Domain.StatusesFlow.Status>>
    {
        public string OrganizationId { get; }

        public GetStatusesQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}