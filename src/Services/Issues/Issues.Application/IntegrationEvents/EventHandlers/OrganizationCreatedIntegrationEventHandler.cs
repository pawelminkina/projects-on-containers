using System.Threading.Tasks;
using EventBus.Abstraction;
using Issues.Application.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace Issues.Application.IntegrationEvents.EventHandlers;

public class OrganizationCreatedIntegrationEventHandler : IIntegrationEventHandler<OrganizationCreatedIntegrationEvent>
{
    private readonly ILogger<OrganizationCreatedIntegrationEventHandler> _logger;

    public OrganizationCreatedIntegrationEventHandler(ILogger<OrganizationCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Handle(OrganizationCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event: {eventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "IssueService", @event);
    }
}