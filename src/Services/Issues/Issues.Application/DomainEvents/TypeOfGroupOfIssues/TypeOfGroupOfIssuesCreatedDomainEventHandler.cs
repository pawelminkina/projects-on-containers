using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.TypeOfGroupOfIssues
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
            var created = notification.CreatedTypeOfGroupOfIssues;
            if (await _repository.AnyOfTypeOfGroupHasGivenNameAsync(created.Name, created.OrganizationId))
                throw new DomainException(Domain.GroupsOfIssues.TypeOfGroupOfIssues.ErrorMessages.SomeTypeOfGroupAlreadyExistWithName(created.Name));
        }
    }
}
