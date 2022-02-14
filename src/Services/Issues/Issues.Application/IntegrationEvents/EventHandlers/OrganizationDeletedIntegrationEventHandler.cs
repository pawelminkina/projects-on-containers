using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using EventBus.Abstraction;
using Issues.Application.Common.Services.DataCleaners;
using Issues.Application.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace Issues.Application.IntegrationEvents.EventHandlers
{
    public class OrganizationDeletedIntegrationEventHandler : IIntegrationEventHandler<OrganizationDeletedIntegrationEvent>
    {
        private readonly IOrganizationDataCleaner _cleaner;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrganizationDeletedIntegrationEventHandler> _logger;

        public OrganizationDeletedIntegrationEventHandler(IOrganizationDataCleaner cleaner, IUnitOfWork unitOfWork, ILogger<OrganizationDeletedIntegrationEventHandler> logger)
        {
            _cleaner = cleaner;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task Handle(OrganizationDeletedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {eventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "IssueService", @event);

            await _cleaner.CleanAsync(@event.OrganizationId);
            await _unitOfWork.CommitAsync();
        }
    }
}
