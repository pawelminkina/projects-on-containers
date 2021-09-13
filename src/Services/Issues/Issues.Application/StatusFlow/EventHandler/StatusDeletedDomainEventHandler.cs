using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class StatusDeletedDomainEventHandler : INotificationHandler<StatusDeletedDomainEvent>
    {
        private readonly IStatusRepository _repository;
        private readonly IStatusFlowRepository _statusFlowRepository;

        public StatusDeletedDomainEventHandler(IStatusRepository repository, IStatusFlowRepository statusFlowRepository)
        {
            _repository = repository;
            _statusFlowRepository = statusFlowRepository;
        }
        
        public async Task Handle(StatusDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var allFlowsForOrg = await _statusFlowRepository.GetFlowsByOrganizationAsync(notification.Status.OrganizationId);
            foreach (var statusFlow in allFlowsForOrg)
            {
                if (statusFlow.StatusesInFlow.Any(d=>d.ParentStatusId == notification.Status.Id))
                    throw new InvalidOperationException($"Requested status to delete is part of flow with id: {statusFlow.Id}");
            }

            await _repository.RemoveStatusById(notification.Status.Id);
        }
    }
}