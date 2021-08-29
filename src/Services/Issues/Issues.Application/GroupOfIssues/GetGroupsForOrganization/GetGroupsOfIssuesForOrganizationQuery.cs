using System.Collections.Generic;
using MediatR;

namespace Issues.Application.GroupOfIssues.GetGroupsForOrganization
{
    public class GetGroupsOfIssuesForOrganizationQuery : IRequest<IEnumerable<Domain.GroupsOfIssues.GroupOfIssues>>
    {
        public GetGroupsOfIssuesForOrganizationQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}