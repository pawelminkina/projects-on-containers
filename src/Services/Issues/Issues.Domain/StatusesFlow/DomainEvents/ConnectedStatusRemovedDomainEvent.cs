using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    //TODO this event should remove connected status in second status
    //for ex. if in parent status was removed connected status with direction In
    //then in connected status there be removed parent status with direction out
    public class ConnectedStatusRemovedDomainEvent : DomainEventBase
    {
        public StatusInFlowConnection Connection { get; }

        public ConnectedStatusRemovedDomainEvent(StatusInFlowConnection connection)
        {
            Connection = connection;
        }
    }
}
