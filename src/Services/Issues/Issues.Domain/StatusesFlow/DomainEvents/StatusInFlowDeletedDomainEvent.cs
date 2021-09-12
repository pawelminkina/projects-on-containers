using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusInFlowDeletedDomainEvent : DomainEventBase
    {
        public StatusInFlow StatusInFlow { get; }

        public StatusInFlowDeletedDomainEvent(StatusInFlow statusInFlow)
        {
            StatusInFlow = statusInFlow;
        }
    }
}