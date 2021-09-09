using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesArchivedDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues TypeOfGroupOfIssues { get; }

        public TypeOfGroupOfIssuesArchivedDomainEvent(TypeOfGroupOfIssues typeOfGroupOfIssues)
        {
            TypeOfGroupOfIssues = typeOfGroupOfIssues;
        }
    }
}