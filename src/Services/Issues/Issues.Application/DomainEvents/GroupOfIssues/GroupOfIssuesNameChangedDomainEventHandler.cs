﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.DomainEvents.GroupOfIssues
{
    public class GroupOfIssuesNameChangedDomainEventHandler : INotificationHandler<GroupOfIssuesNameChangedDomainEvent>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;

        public GroupOfIssuesNameChangedDomainEventHandler(IGroupOfIssuesRepository groupOfIssuesRepository)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
        }
        public async Task Handle(GroupOfIssuesNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var changedGroupOfIssues = notification.ChangedGroupOfIssues;
            var organizationId = changedGroupOfIssues.TypeOfGroup.OrganizationId;
            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenNameAsync(changedGroupOfIssues.Name, organizationId))
                throw new AlreadyExistException(Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithName(changedGroupOfIssues.Name));

            changedGroupOfIssues.ConnectedStatusFlow.Rename(Domain.StatusesFlow.StatusFlow.GetNameWithGroupOfIssues(changedGroupOfIssues.Name));
        }
    }
}
