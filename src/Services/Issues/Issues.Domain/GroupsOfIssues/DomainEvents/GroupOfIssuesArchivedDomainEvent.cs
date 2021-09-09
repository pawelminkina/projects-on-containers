using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class GroupOfIssuesArchivedDomainEvent : DomainEventBase
    {
        public GroupOfIssues GroupOfIssues { get; }

        public GroupOfIssuesArchivedDomainEvent(GroupOfIssues groupOfIssues)
        {
            GroupOfIssues = groupOfIssues;
        }
    }
}