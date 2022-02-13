using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstraction;
using Issues.Application.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace Issues.Application.IntegrationEvents.EventHandlers
{
    public class OrganizationDeletedIntegrationEventHandler : IIntegrationEventHandler<OrganizationDeletedIntegrationEvent>
    {
        private readonly ILogger<OrganizationDeletedIntegrationEventHandler> _logger;

        public OrganizationDeletedIntegrationEventHandler(ILogger<OrganizationDeletedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(OrganizationDeletedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {eventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "IssueService", @event);
        }
    }
}
