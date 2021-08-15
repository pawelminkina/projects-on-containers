using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DDD.Events
{
    public abstract class DomainEventBase : IDomainEvent
    {
        protected DomainEventBase()
        {
            this.OccurredOn = DateTimeOffset.Now;
        }

        public DateTimeOffset OccurredOn { get; }
    }
}
