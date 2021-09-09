using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusFlowArchivedDomainEvent : DomainEventBase
    {
        public StatusFlow Flow { get; }

        public StatusFlowArchivedDomainEvent(StatusFlow flow)
        {
            Flow = flow;
        }
    }
}