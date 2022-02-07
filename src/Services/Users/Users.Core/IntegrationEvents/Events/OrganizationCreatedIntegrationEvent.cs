using EventBus.Events;

namespace Users.Core.IntegrationEvents.Events
{
    public class OrganizationCreatedIntegrationEvent : IntegrationEvent
    {
        public OrganizationCreatedIntegrationEvent(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}
