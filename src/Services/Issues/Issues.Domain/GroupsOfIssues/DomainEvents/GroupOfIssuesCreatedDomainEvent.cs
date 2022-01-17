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

        public GroupOfIssuesCreatedDomainEvent(GroupOfIssues created)
        {
            Created = created;
        }
    }
}
