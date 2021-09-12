using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class TypeOfGroupOfIssuesArchivedDomainEvent : DomainEventBase
    {
        public TypeOfGroupOfIssues TypeOfGroupOfIssues { get; }
        public TypeOfGroupOfIssues TypeWhereGroupsWillBeMoved { get; }

        public TypeOfGroupOfIssuesArchivedDomainEvent(TypeOfGroupOfIssues typeOfGroupOfIssues, TypeOfGroupOfIssues typeWhereGroupsWillBeMoved)
        {
            TypeOfGroupOfIssues = typeOfGroupOfIssues;
            TypeWhereGroupsWillBeMoved = typeWhereGroupsWillBeMoved;
        }
    }
}