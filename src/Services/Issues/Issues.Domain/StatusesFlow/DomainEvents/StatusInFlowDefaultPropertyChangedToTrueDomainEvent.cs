using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    public class StatusInFlowDefaultPropertyChangedToTrueDomainEvent : DomainEventBase
    {
        public StatusInFlow Status { get; }

        public StatusInFlowDefaultPropertyChangedToTrueDomainEvent(StatusInFlow status)
        {
            //TODO check is this the only default status in given status flow
            Status = status;
        }
    }
}
