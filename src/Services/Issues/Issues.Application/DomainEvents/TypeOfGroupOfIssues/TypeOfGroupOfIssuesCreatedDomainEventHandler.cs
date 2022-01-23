using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                throw new InvalidOperationException($"Type of group of issues with name: {created.Name} already exist");
        }
    }
}
