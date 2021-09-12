using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesUnsettedFromDefaultDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues Type { get; }

        public TypeOfGroupOfIssuesUnsettedFromDefaultDomainEvent(TypeOfGroupOfIssues type)
        {
            Type = type;
        }
    }
}