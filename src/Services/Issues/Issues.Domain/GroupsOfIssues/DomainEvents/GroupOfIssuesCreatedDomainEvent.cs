using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class GroupOfIssuesCreatedDomainEvent : DomainEventBase
    {
        public GroupOfIssues Created { get; }
        //TODO this event should create statusflow and assign it to group of issues

        public GroupOfIssuesCreatedDomainEvent(GroupOfIssues created)
        {
            Created = created;
        }
    }
}
