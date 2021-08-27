using Architecture.DDD.Events;

namespace Issues.Domain.TypesOfIssues.DomainEvents
{
    public class TypeOfIssueInGroupRemovedDomainEvent : DomainEventBase
    {
        public TypeOfIssueInTypeOfGroup TypeToDelete { get; }

        public TypeOfIssueInGroupRemovedDomainEvent(TypeOfIssueInTypeOfGroup typeToDelete)
        {
            TypeToDelete = typeToDelete;
        }
    }
}