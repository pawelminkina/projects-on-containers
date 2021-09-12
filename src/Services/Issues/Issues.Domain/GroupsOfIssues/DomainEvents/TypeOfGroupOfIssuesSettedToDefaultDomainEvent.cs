using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesSettedToDefaultDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues TypeOfGroupOfIssues { get; }

        public TypeOfGroupOfIssuesSettedToDefaultDomainEvent(TypeOfGroupOfIssues typeOfGroupOfIssues)
        {
            TypeOfGroupOfIssues = typeOfGroupOfIssues;
        }
    }
}