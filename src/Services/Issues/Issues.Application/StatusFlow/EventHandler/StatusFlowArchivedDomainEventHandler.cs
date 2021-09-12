using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Domain.StatusesFlow.DomainEvents;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.StatusFlow.EventHandler
{
    public class StatusFlowArchivedDomainEventHandler : INotificationHandler<StatusFlowArchivedDomainEvent>
    {
        private readonly ITypeOfIssueRepository _typeOfIssueRepository;
        private readonly IStatusFlowRepository _statusFlowRepository;

        public StatusFlowArchivedDomainEventHandler(ITypeOfIssueRepository typeOfIssueRepository, IStatusFlowRepository statusFlowRepository)
        {
            _typeOfIssueRepository = typeOfIssueRepository;
            _statusFlowRepository = statusFlowRepository;
        }
        //type of issue where this flow is assigned should have new flow id which is default
        public async Task Handle(StatusFlowArchivedDomainEvent notification, CancellationToken cancellationToken)
        {
            await ChangeTypeOfIssuesFlowToDefault(notification.Flow);
        }

        private async Task ChangeTypeOfIssuesFlowToDefault(Domain.StatusesFlow.StatusFlow flow)
        {
            var flows = await _statusFlowRepository.GetFlowsByOrganizationAsync(flow.OrganizationId);
            var defaultFlow = flows.FirstOrDefault(s => s.IsDefault && s.Id != flow.Id);
            if (defaultFlow is null)
                throw new InvalidOperationException("There is no default flow to which type of issues could be assigned");

            var typeOfIssues = await _typeOfIssueRepository.GetTypeOfIssuesForOrganizationAsync(flow.OrganizationId);
            foreach (var type in typeOfIssues)
            {
                foreach (var typeInGroup in type.TypesInGroups.Where(typeInGroup => typeInGroup.StatusFlowId == flow.Id))
                {
                    typeInGroup.ChangeStatusFlow(defaultFlow);
                }
            }
        }
    }
}