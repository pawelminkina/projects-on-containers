using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.EventHandlers
{
    public class TypeOfGroupOfIssuesSettedToDefaultDomainEventHandler : INotificationHandler<TypeOfGroupOfIssuesSettedToDefaultDomainEvent>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public TypeOfGroupOfIssuesSettedToDefaultDomainEventHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(TypeOfGroupOfIssuesSettedToDefaultDomainEvent notification, CancellationToken cancellationToken)
        {
            var types = await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(notification.TypeOfGroupOfIssues.OrganizationId);
            var typeToChange = types.FirstOrDefault(d => d.IsDefault && d.Id != notification.TypeOfGroupOfIssues.Id);
            
            var settedIsFirstTypeInOrganization = typeToChange is null;
            if (settedIsFirstTypeInOrganization)
                return;
            
            typeToChange.SetIsDefaultToFalse();
        }
    }
}