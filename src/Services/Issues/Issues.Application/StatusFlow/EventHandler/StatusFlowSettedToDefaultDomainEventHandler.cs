using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class StatusFlowSettedToDefaultDomainEventHandler : INotificationHandler<StatusFlowSettedToDefaultDomainEvent>
    {
        private readonly IStatusFlowRepository _repository;

        public StatusFlowSettedToDefaultDomainEventHandler(IStatusFlowRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(StatusFlowSettedToDefaultDomainEvent notification, CancellationToken cancellationToken)
        {
            var flows = await _repository.GetFlowsByOrganizationAsync(notification.StatusFlow.OrganizationId);
            var flowToChange = flows.FirstOrDefault(s => s.IsDefault && s.Id != notification.StatusFlow.Id);

            var settedIsFirstFlowInOrganization = flowToChange is null;
            if (settedIsFirstFlowInOrganization)
                return;

            flowToChange.SetIsDefaultToFalse();
        }
    }
}