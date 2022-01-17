using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class GroupOfIssuesNameChangedDomainEvent : DomainEventBase
    {
        public GroupOfIssues ChangedGroupOfIssues { get; }

        public GroupOfIssuesNameChangedDomainEvent(GroupOfIssues changedGroupOfIssues)
        {
            ChangedGroupOfIssues = changedGroupOfIssues;
        }
    }
}
