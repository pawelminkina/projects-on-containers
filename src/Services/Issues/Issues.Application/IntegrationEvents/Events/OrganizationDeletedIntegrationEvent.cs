using EventBus.Events;

namespace Issues.Application.IntegrationEvents.Events;

public class OrganizationDeletedIntegrationEvent : IntegrationEvent
{
    public OrganizationDeletedIntegrationEvent(string organizationId)
    {
        OrganizationId = organizationId;
    }

    public string OrganizationId { get; }
}