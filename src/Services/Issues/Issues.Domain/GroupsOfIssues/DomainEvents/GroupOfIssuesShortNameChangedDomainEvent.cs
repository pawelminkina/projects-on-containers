using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class GroupOfIssuesShortNameChangedDomainEvent : DomainEventBase
    {
        public GroupOfIssues ChangedGroupOfIssues { get; }
        //todo shortname changed event domain, and check that any of those already exist in db _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync
        //if (await _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync(request.NewShortName, request.OrganizationId))
        //    throw new InvalidOperationException("Group with requested short name already exist");

        public GroupOfIssuesShortNameChangedDomainEvent(GroupOfIssues changedGroupOfIssues)
        {
            ChangedGroupOfIssues = changedGroupOfIssues;
        }
    }
}
