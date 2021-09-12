using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.EventHandlers
{
    public class TypeOfGroupOfIssuesUnsettedFromDefaultDomainEventHandler : INotificationHandler<TypeOfGroupOfIssuesUnsettedFromDefaultDomainEvent>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public TypeOfGroupOfIssuesUnsettedFromDefaultDomainEventHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(TypeOfGroupOfIssuesUnsettedFromDefaultDomainEvent notification, CancellationToken cancellationToken)
        {
            var types = await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(notification.Type.OrganizationId);
            var newDefaultTypeAdded = types.Any(d => d.IsDefault && d.Id != notification.Type.Id);
            if (newDefaultTypeAdded)
                return;

            throw new InvalidOperationException("There is no other default type added");
        }
    }
}