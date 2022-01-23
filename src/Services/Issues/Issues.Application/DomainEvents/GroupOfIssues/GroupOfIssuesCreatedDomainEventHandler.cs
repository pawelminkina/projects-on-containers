using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.DomainEvents.GroupOfIssues
{
    public class GroupOfIssuesCreatedDomainEventHandler : INotificationHandler<GroupOfIssuesCreatedDomainEvent>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IStatusFlowRepository _statusFlowRepository;

        public GroupOfIssuesCreatedDomainEventHandler(IGroupOfIssuesRepository groupOfIssuesRepository, IStatusFlowRepository statusFlowRepository)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _statusFlowRepository = statusFlowRepository;
        }
        public async Task Handle(GroupOfIssuesCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var createdGroupOfIssues = notification.Created;
            var organizationId = createdGroupOfIssues.TypeOfGroup.OrganizationId;

            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync(createdGroupOfIssues.ShortName, organizationId))
                throw new InvalidOperationException($"Group of issues with short name: {createdGroupOfIssues.ShortName} already exist");

            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenNameAsync(createdGroupOfIssues.Name, organizationId))
                throw new InvalidOperationException($"Group of issues with name: {createdGroupOfIssues.Name} already exist");

            var defaultStatusFlow = await _statusFlowRepository.GetDefaultStatusFlowAsync(organizationId);
            var statusNames = defaultStatusFlow.StatusesInFlow.Select(d => d.Name);
            var defaultStatusInFlowName = defaultStatusFlow.StatusesInFlow.First(d => d.IsDefault).Name;

            var statusFlow = new Domain.StatusesFlow.StatusFlow(Domain.StatusesFlow.StatusFlow.GetNameWithGroupOfIssues(createdGroupOfIssues.Name), organizationId, createdGroupOfIssues, statusNames, defaultStatusInFlowName);
            createdGroupOfIssues.ConnectedStatusFlowId = statusFlow.Id;

            await _statusFlowRepository.AddNewStatusFlowAsync(statusFlow);
        }
    }
}
