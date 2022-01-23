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
    public class DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEventHandler : INotificationHandler<DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEvent>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEventHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEvent notification, CancellationToken cancellationToken)
        {
            var allTypes = await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(notification.TargetedTypeOfGroupOfIssues.OrganizationId);
            var allDefaultTypesCount = allTypes.Count(s => s.IsDefault);

            if (allDefaultTypesCount == 1)
                return;
            
            throw new InvalidOperationException($"There is {allDefaultTypesCount} default types of group of issues in database for given organization, but should be one");
        }
    }
}
