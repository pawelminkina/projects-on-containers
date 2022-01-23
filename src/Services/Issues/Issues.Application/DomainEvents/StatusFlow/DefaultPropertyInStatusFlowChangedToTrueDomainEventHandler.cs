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
    public class DefaultPropertyInStatusFlowChangedToTrueDomainEventHandler : INotificationHandler<DefaultPropertyInStatusFlowChangedToTrueDomainEvent>
    {
        private readonly IStatusFlowRepository _statusFlowRepository;

        public DefaultPropertyInStatusFlowChangedToTrueDomainEventHandler(IStatusFlowRepository statusFlowRepository)
        {
            _statusFlowRepository = statusFlowRepository;
        }
        public async Task Handle(DefaultPropertyInStatusFlowChangedToTrueDomainEvent notification, CancellationToken cancellationToken)
        {
            var allTypes = await _statusFlowRepository.GetFlowsByOrganizationAsync(notification.TargetedStatusFlow.OrganizationId);
            var allDefaultTypesCount = allTypes.Count(s => s.IsDefault);

            if (allDefaultTypesCount == 1)
                return;

            throw new InvalidOperationException($"There is {allDefaultTypesCount} default status flows in database for given organization, but should be one");
        }
    }
}
