using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.GroupOfIssues
{
    public class GroupOfIssuesUndoDeletedDomainEventHandler : INotificationHandler<GroupOfIssuesUndoDeletedDomainEvent>
    {
        public async Task Handle(GroupOfIssuesUndoDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            notification.TargetedGroup.ConnectedStatusFlow.UnDelete();
        }
    }
}
