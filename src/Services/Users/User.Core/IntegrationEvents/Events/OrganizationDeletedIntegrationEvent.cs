using EventBus.Events;

namespace User.Core.IntegrationEvents.Events
{
    public class OrganizationDeletedIntegrationEvent : IntegrationEvent
    {
        public OrganizationDeletedIntegrationEvent(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}
