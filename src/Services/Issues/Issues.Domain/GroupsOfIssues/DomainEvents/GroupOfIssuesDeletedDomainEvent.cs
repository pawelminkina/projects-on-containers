using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    //TODO archive status flow
    public class GroupOfIssuesDeletedDomainEvent : DomainEventBase
    {
        public GroupOfIssues DeletedGroup { get; }

        public GroupOfIssuesDeletedDomainEvent(GroupOfIssues deletedGroup)
        {
            DeletedGroup = deletedGroup;
        }
    }
}
