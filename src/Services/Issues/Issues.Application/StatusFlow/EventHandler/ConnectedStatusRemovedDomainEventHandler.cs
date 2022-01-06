using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class ConnectedStatusRemovedDomainEventHandler : INotificationHandler<ConnectedStatusRemovedDomainEvent>
    {
        public ConnectedStatusRemovedDomainEventHandler()
        {

        }

        public async Task Handle(ConnectedStatusAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var statusFromFlow = notification.Connection.ParentStatusInFlow.StatusFlow.StatusesInFlow.FirstOrDefault(d => d.ParentStatusId == notification.Connection.ConnectedStatusId);

            if (statusFromFlow is null)
                throw new InvalidOperationException("Connected status has not been found");

            statusFromFlow.AddConnectedStatus(notification.Connection.ParentStatus, StatusInFlowDirection.In);
        }

        public async Task Handle(ConnectedStatusRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            var statusFromFlow = notification.Connection.ParentStatusInFlow.StatusFlow.StatusesInFlow.Select(s=>s.ConnectedStatuses).Where(s=>s.Id == notification.Connection.ParentStatusId));
        }
    }
}
