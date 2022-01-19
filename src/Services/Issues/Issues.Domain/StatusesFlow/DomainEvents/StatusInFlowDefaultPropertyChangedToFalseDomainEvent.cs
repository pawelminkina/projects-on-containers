using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusInFlowDefaultPropertyChangedToFalseDomainEvent : DomainEventBase
    {
        public StatusInFlow ChangedStatusFlow { get; }
        //TODO check that in this flow already exist new status which has default set to true, if not fail operation
        public StatusInFlowDefaultPropertyChangedToFalseDomainEvent(StatusInFlow changedStatusFlow)
        {
            ChangedStatusFlow = changedStatusFlow;
        }
    }
}
