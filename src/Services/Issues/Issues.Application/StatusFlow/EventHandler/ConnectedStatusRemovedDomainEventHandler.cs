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

        public async Task Handle(ConnectedStatusRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            var status = notification.Connection.ParentStatusInFlow.StatusFlow.StatusesInFlow.FirstOrDefault(s=>s.ParentStatus.Id == notification.Connection.ConnectedStatus.Id);
            if (status == null)
                throw new InvalidOperationException("There was no connected statuses to delete");
            //im searching for connected status in statusinflow
            status.DeleteConnectedStatus(notification.Connection.ParentStatusInFlow.ParentStatus.Id, StatusInFlowDirection.In);
        }
    }
}
