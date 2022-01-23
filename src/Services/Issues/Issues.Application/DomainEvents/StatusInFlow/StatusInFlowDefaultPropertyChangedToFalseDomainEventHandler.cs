using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.StatusInFlow
{
    public class StatusInFlowDefaultPropertyChangedToFalseDomainEventHandler : INotificationHandler<StatusInFlowDefaultPropertyChangedToFalseDomainEvent>
    {
        public async Task Handle(StatusInFlowDefaultPropertyChangedToFalseDomainEvent notification, CancellationToken cancellationToken)
        {
            var isAnyNewDefaultStatusInFlow = notification.ChangedStatusFlow.StatusFlow.StatusesInFlow.Any(d=>d.IsDefault && d.Id != notification.ChangedStatusFlow.Id);
            
            if (!isAnyNewDefaultStatusInFlow)
                throw new InvalidOperationException($"There is no new status in flow in status flow with id: {notification.ChangedStatusFlow.StatusFlow.Id}");
        }
    }
}
