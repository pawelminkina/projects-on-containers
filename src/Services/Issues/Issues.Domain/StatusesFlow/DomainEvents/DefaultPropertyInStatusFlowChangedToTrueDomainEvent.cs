using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class DefaultPropertyInStatusFlowChangedToTrueDomainEvent : DomainEventBase
    {
        public StatusFlow TargetedStatusFlow { get; }

        public DefaultPropertyInStatusFlowChangedToTrueDomainEvent(StatusFlow targetedStatusFlow)
        {
            TargetedStatusFlow = targetedStatusFlow;
        }
    }
}
