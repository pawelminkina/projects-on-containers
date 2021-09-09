using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class MoveIssuesToGroupDomainEvent : DomainEventBase
    {
        private readonly GroupOfIssues _groupOfIssues;
        private readonly string _groupIdToWhichIssuesShouldBeMoved;

        public MoveIssuesToGroupDomainEvent(GroupOfIssues groupOfIssues, string groupIdToWhichIssuesShouldBeMoved)
        {
            _groupOfIssues = groupOfIssues;
            _groupIdToWhichIssuesShouldBeMoved = groupIdToWhichIssuesShouldBeMoved;
        }
    }
}