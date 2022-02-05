using EventBus.Events;

namespace User.Core.IntegrationEvents.Events
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
