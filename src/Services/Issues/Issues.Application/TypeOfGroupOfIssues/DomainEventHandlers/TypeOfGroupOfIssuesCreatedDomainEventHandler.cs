using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.Events;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.DomainEventHandlers
{
    public class TypeOfGroupOfIssuesCreatedDomainEventHandler : INotificationHandler<TypeOfGroupOfIssuesCreatedDomainEvent>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public TypeOfGroupOfIssuesCreatedDomainEventHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(TypeOfGroupOfIssuesCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.AddNewTypeofGroupOfIssuesAsync(notification.Type);
        }
    }
}