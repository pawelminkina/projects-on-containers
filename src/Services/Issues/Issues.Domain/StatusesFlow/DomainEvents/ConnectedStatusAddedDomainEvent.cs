using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.StatusesFlow.DomainEvents
{
    //TODO this event should add connected status in second status
    //for ex. if in parent status was added connected status with direction In
    //then in connected status there be added parent status with direction out
    public class ConnectedStatusAddedDomainEvent : DomainEventBase
    {
        public StatusInFlowConnection Connection { get; }

        public ConnectedStatusAddedDomainEvent(StatusInFlowConnection connection)
        {
            Connection = connection;
        }
    }
}
