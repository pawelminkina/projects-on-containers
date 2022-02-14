using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using EventBus.Abstraction;
using Issues.Application.IntegrationEvents.Events;
using Issues.Domain;
using Issues.Domain.Dtos;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Microsoft.Extensions.Logging;

namespace Issues.Application.IntegrationEvents.EventHandlers;

public class OrganizationCreatedIntegrationEventHandler : IIntegrationEventHandler<OrganizationCreatedIntegrationEvent>
{
    private readonly ITypeOfGroupOfIssuesRepository _typeOfGroupOfIssuesRepository;
    private readonly IStatusFlowRepository _statusFlowRepository;
    private readonly ILogger<OrganizationCreatedIntegrationEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationCreatedIntegrationEventHandler(ITypeOfGroupOfIssuesRepository typeOfGroupOfIssuesRepository, IStatusFlowRepository statusFlowRepository, ILogger<OrganizationCreatedIntegrationEventHandler> logger, IUnitOfWork unitOfWork)
    {
        _typeOfGroupOfIssuesRepository = typeOfGroupOfIssuesRepository;
        _statusFlowRepository = statusFlowRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(OrganizationCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event: {eventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "IssueService", @event);

        //There can be a business need use some specific language for created organization, for ex. Polish
        //Then I can create a service which will retrieve default values from some data source
        var defaultType = DefaultEntityCreator.CreateDefaultTypeOfGroupOfIssues("None", @event.OrganizationId);
        await _typeOfGroupOfIssuesRepository.AddNewTypeofGroupOfIssuesAsync(defaultType);

        var defaultStatusFlow = DefaultEntityCreator.CreateDefaultStatusFlow("Default", @event.OrganizationId, GetDefaultStatuses());
        await _statusFlowRepository.AddNewStatusFlowAsync(defaultStatusFlow);
        
        await _unitOfWork.CommitAsync();
    }

    private IEnumerable<StatusInFlowToCreateDto> GetDefaultStatuses()
    {
        return new List<StatusInFlowToCreateDto>()
        {
            new StatusInFlowToCreateDto()
            {
                IsDefault = true,
                StatusName = "To do",
                ConnectedStatuses = new List<string>()
                {
                    "Done"
                }
            },
            new StatusInFlowToCreateDto()
            {
                IsDefault = false,
                StatusName = "Done",
                ConnectedStatuses = new List<string>()
                {
                    "To do"
                }
            }
        };
    }
}