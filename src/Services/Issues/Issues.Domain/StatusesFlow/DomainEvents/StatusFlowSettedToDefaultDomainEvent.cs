using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusFlowSettedToDefaultDomainEvent : DomainEventBase
    {
        public StatusFlow StatusFlow { get; }

        public StatusFlowSettedToDefaultDomainEvent(StatusFlow statusFlow)
        {
            StatusFlow = statusFlow;
        }
    }
}