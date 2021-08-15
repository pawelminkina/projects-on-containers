using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DDD.Events
{
    public interface IDomainEvent : INotification
    {
    }
}
