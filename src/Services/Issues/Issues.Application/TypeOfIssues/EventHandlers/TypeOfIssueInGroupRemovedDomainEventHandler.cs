using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.TypesOfIssues;
using Issues.Domain.TypesOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.TypeOfIssues.EventHandlers
{
    public class TypeOfIssueInGroupRemovedDomainEventHandler : INotificationHandler<TypeOfIssueInGroupRemovedDomainEvent>
    {
        private readonly ITypeOfIssueRepository _repository;

        public TypeOfIssueInGroupRemovedDomainEventHandler(ITypeOfIssueRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(TypeOfIssueInGroupRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveTypeOfIssueInTypeofGroupOfIssues(notification.TypeToDelete);
        }
    }
}