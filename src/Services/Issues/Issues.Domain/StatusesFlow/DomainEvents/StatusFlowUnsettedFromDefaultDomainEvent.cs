using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusFlowUnsettedFromDefaultDomainEvent : DomainEventBase
    {
        public StatusFlow StatusFlow { get; }

        public StatusFlowUnsettedFromDefaultDomainEvent(StatusFlow statusFlow)
        {
            StatusFlow = statusFlow;
        } 
    }
}