using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusInFlowDeletedDomainEvent : DomainEventBase
    {
        public StatusInFlow StatusInFlow { get; }

        //TODO delete all connections in database which have this status
        public StatusInFlowDeletedDomainEvent(StatusInFlow statusInFlow)
        {
            StatusInFlow = statusInFlow;
        }
    }
}