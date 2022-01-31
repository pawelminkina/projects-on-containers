using System.Collections.Generic;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusInFlowDeletedDomainEvent : DomainEventBase
    {
        public StatusInFlow StatusInFlow { get; }
        public IEnumerable<StatusInFlow> AllStatusesFromFlow { get; }

        //TODO delete all connections in database which have this status
        public StatusInFlowDeletedDomainEvent(StatusInFlow statusInFlow, IEnumerable<StatusInFlow> allStatusesFromFlow)
        {
            StatusInFlow = statusInFlow;
            AllStatusesFromFlow = allStatusesFromFlow;
        }
    }
}