using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.EventHandlers
{
    public class TypeOfGroupOfIssuesArchivedDomainEventHandler : INotificationHandler<TypeOfGroupOfIssuesArchivedDomainEvent>
    {
        public async Task Handle(TypeOfGroupOfIssuesArchivedDomainEvent notification, CancellationToken cancellationToken)
        { 
            notification.TypeWhereGroupsWillBeMoved.AddExistingGroupsOfIssues(notification.TypeOfGroupOfIssues.Groups.ToList());
        }
    }
}