using System.Collections;
using System.Collections.Generic;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.GetIssuesForGroup
{
    public class GetIssuesForGroupQuery : IRequest<IEnumerable<Issue>>
    {
        public GetIssuesForGroupQuery(string groupId, string organizationId)
        {
            GroupId = groupId;
            OrganizationId = organizationId;
        }
        public string GroupId { get; }
        public string OrganizationId { get; }

    }
}