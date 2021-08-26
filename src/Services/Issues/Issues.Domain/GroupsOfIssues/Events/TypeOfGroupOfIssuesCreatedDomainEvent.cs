using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.Events
{
    public class TypeOfGroupOfIssuesCreatedDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues Type { get; }

        public TypeOfGroupOfIssuesCreatedDomainEvent(TypeOfGroupOfIssues type)
        {
            Type = type;
        }


    }
}