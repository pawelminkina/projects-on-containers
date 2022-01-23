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
    public class StatusInFlowDefaultPropertyChangedToTrueDomainEventHandler : INotificationHandler<StatusInFlowDefaultPropertyChangedToTrueDomainEvent>
    {
        public async Task Handle(StatusInFlowDefaultPropertyChangedToTrueDomainEvent notification, CancellationToken cancellationToken)
        {
            var isAnyNewDefaultStatusInFlow = notification.Status.StatusFlow.StatusesInFlow.Any(d=>d.IsDefault && d.Id != notification.Status.Id);

            if(isAnyNewDefaultStatusInFlow)
                throw new InvalidOperationException($"There is another status with default status in status flow with id: {notification.Status.Id}");
        }
    }
}
