using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class StatusInFlowDeletedDomainEventHandler : INotificationHandler<StatusInFlowDeletedDomainEvent>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;

        public StatusInFlowDeletedDomainEventHandler(IStatusFlowRepository statusFlowRepository)
        {
            _statusFlowRepository = statusFlowRepository;
        }
        public async Task Handle(StatusInFlowDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _statusFlowRepository.RemoveStatusInFlow(notification.StatusInFlow.Id);
            var statuses = notification.StatusInFlow.StatusFlow.StatusesInFlow;
            foreach (var status in statuses)
            {
                status.DeleteConnectedStatus(notification.StatusInFlow.ParentStatus.Id, StatusInFlowDirection.In);
            }
        }
    }
}