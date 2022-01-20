using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues TargetedTypeOfGroupOfIssues { get; }
        //TODO checks that this is the only default typeofgroupofissues is organization
        public DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEvent(TypeOfGroupOfIssues targetedTypeOfGroupOfIssues)
        {
            TargetedTypeOfGroupOfIssues = targetedTypeOfGroupOfIssues;
        }
    }
}
