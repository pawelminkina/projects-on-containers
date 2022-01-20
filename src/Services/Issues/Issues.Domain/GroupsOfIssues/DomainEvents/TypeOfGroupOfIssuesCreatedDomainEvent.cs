using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesCreatedDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues CreatedTypeOfGroupOfIssues { get; }

        //TODO check that name is unique within organization (repository has method for that)
        public TypeOfGroupOfIssuesCreatedDomainEvent(TypeOfGroupOfIssues createdTypeOfGroupOfIssues)
        {
            CreatedTypeOfGroupOfIssues = createdTypeOfGroupOfIssues;
        }
    }
}
