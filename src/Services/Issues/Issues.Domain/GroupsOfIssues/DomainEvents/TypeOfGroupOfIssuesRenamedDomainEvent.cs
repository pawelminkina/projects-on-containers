using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesRenamedDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues CreatedTypeOfGroupOfIssues { get; }

        //TODO check that name of type of group of issues is unique within organization
        public TypeOfGroupOfIssuesRenamedDomainEvent(TypeOfGroupOfIssues createdTypeOfGroupOfIssues)
        {
            CreatedTypeOfGroupOfIssues = createdTypeOfGroupOfIssues;
        }
    }
}
