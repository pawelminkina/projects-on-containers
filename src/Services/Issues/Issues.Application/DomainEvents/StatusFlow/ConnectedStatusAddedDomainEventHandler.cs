using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.StatusFlow
{
    public class ConnectedStatusAddedDomainEventHandler : INotificationHandler<ConnectedStatusAddedDomainEvent>
    {
        public async Task Handle(ConnectedStatusAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            notification.Connection.ConnectedStatusInFlow.AddConnectedStatus(notification.Connection.ParentStatusInFlow, GetOppositeDirection(notification.Connection.Direction));
        }

        private static StatusInFlowDirection GetOppositeDirection(StatusInFlowDirection direction) =>
            direction == StatusInFlowDirection.In ? StatusInFlowDirection.Out : StatusInFlowDirection.In;

    }
}
