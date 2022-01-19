using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Events;

namespace Issues.Domain.GroupsOfIssues.DomainEvents
{
    public class GroupOfIssuesCreatedDomainEvent : DomainEventBase
    {
        public GroupOfIssues Created { get; }
        //TODO this event should create statusflow and assign it to group of issues
        //TODO _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync
        //TODO _groupOfIssuesRepository.AnyOfGroupHasGivenNameAsync
        //if (await _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync(request.ShortName, request.OrganizationId))
        //    throw new InvalidOperationException($"Group of issues with short name: {request.ShortName} already exist");

        //if (await _groupOfIssuesRepository.AnyOfGroupHasGivenNameAsync(request.Name, request.OrganizationId))
        //    throw new InvalidOperationException($"Group of issues with name: {request.Name} already exist");

        public GroupOfIssuesCreatedDomainEvent(GroupOfIssues created)
        {
            Created = created;
        }
    }
}
