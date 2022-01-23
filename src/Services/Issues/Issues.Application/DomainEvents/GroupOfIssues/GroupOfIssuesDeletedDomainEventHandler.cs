using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.DomainEvents.GroupOfIssues
{
    public class GroupOfIssuesDeletedDomainEventHandler : INotificationHandler<GroupOfIssuesDeletedDomainEvent>
    {
        public async Task Handle(GroupOfIssuesDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            notification.DeletedGroup.ConnectedStatusFlow.Delete();
        }
    }
}
