using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{            
    //TODO undo archive status flow
    public class GroupOfIssuesUndoDeletedDomainEvent : DomainEventBase
    {
        public GroupOfIssues TargetedGroup { get; }

        public GroupOfIssuesUndoDeletedDomainEvent(GroupOfIssues targetedGroup)
        {
            TargetedGroup = targetedGroup;
        }
    }
}
