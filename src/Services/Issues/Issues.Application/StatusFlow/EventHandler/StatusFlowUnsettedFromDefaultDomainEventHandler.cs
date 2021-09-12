using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class StatusFlowUnsettedFromDefaultDomainEventHandler : INotificationHandler<StatusFlowUnsettedFromDefaultDomainEvent>
    {
        private readonly IStatusFlowRepository _repository;

        public StatusFlowUnsettedFromDefaultDomainEventHandler(IStatusFlowRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(StatusFlowUnsettedFromDefaultDomainEvent notification, CancellationToken cancellationToken)
        {
            var types = await _repository.GetFlowsByOrganizationAsync(notification.StatusFlow.OrganizationId);
            var newDefaultFlowAdded = types.Any(d => d.IsDefault && d.Id != notification.StatusFlow.Id);
            if (newDefaultFlowAdded)
                return;

            throw new InvalidOperationException("There is no other default flow added");
        }
    }
}