using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.GroupOfIssues
{
    public class GroupOfIssuesShortNameChangedDomainEventHandler : INotificationHandler<GroupOfIssuesShortNameChangedDomainEvent>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;

        public GroupOfIssuesShortNameChangedDomainEventHandler(IGroupOfIssuesRepository groupOfIssuesRepository)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
        }
        public async Task Handle(GroupOfIssuesShortNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var changedGroup = notification.ChangedGroupOfIssues;
            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync(changedGroup.ShortName, changedGroup.TypeOfGroup.OrganizationId))
                throw new InvalidOperationException($"Group of issues with short name: {changedGroup.ShortName} already exist");
        }
    }
}
