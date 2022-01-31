using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.StatusInFlow
{
    public class StatusInFlowDeletedDomainEventHandler : INotificationHandler<StatusInFlowDeletedDomainEvent>
    {
        public async Task Handle(StatusInFlowDeletedDomainEvent notification, CancellationToken cancellationToken)
        {

            foreach (var statusInFlow in notification.AllStatusesFromFlow)
            {
                if (statusInFlow.ConnectedStatuses.Any(s=>s.ConnectedStatusInFlow.Id == notification.StatusInFlow.Id))
                    statusInFlow.DeleteConnectedStatus(notification.StatusInFlow);
            }
        }
    }
}
