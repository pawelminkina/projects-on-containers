using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusDeletedDomainEvent : DomainEventBase
    {
        public Status Status { get; }

        public StatusDeletedDomainEvent(Status status)
        {
            Status = status;
        }
    }
}