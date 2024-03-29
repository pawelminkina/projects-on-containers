﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using Issues.Application.Common.Exceptions;
using Issues.Domain.Dtos;
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
                throw new AlreadyExistException(Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithShortName(createdGroupOfIssues.ShortName));

            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenNameAsync(createdGroupOfIssues.Name, organizationId))
                throw new AlreadyExistException(Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithName(createdGroupOfIssues.Name));

            var defaultStatusFlow = await _statusFlowRepository.GetDefaultStatusFlowAsync(organizationId);
            
            var statusesToAdd = defaultStatusFlow.StatusesInFlow.Select(s => new StatusInFlowToCreateDto()
            {
                IsDefault = s.IsDefault,
                StatusName = s.Name,
                ConnectedStatuses = s.ConnectedStatuses.Select(d => d.ConnectedStatusInFlow.Name)
            });

            var statusFlow = new Domain.StatusesFlow.StatusFlow(Domain.StatusesFlow.StatusFlow.GetNameWithGroupOfIssues(createdGroupOfIssues.Name), organizationId, createdGroupOfIssues, statusesToAdd);

            await _statusFlowRepository.AddNewStatusFlowAsync(statusFlow);
        }
    }
}
