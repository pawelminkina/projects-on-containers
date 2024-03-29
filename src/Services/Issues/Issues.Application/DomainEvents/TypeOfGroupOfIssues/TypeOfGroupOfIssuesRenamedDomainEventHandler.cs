﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using MediatR;

namespace Issues.Application.DomainEvents.TypeOfGroupOfIssues
{
    public class TypeOfGroupOfIssuesRenamedDomainEventHandler : INotificationHandler<TypeOfGroupOfIssuesRenamedDomainEvent>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public TypeOfGroupOfIssuesRenamedDomainEventHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(TypeOfGroupOfIssuesRenamedDomainEvent notification, CancellationToken cancellationToken)
        {
            var changed = notification.ChangedTypeOfGroupOfIssues;
            if (await _repository.AnyOfTypeOfGroupHasGivenNameAsync(changed.Name, changed.OrganizationId))
                throw new AlreadyExistException(Domain.GroupsOfIssues.TypeOfGroupOfIssues.ErrorMessages.SomeTypeOfGroupAlreadyExistWithName(changed.Name));
        }
    }
}
