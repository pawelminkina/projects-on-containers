﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.CQRS.GroupOfIssues.Queries.GetGroup
{
    public class GetGroupOfIssuesQuery : IRequest<Domain.GroupsOfIssues.GroupOfIssues>
    {
        public GetGroupOfIssuesQuery(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }
    }

    public class GetGroupOfIssuesQueryHandler : IRequestHandler<GetGroupOfIssuesQuery, Domain.GroupsOfIssues.GroupOfIssues>
    {
        private readonly IGroupOfIssuesRepository _repository;

        public GetGroupOfIssuesQueryHandler(IGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Domain.GroupsOfIssues.GroupOfIssues> Handle(GetGroupOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetGroupOfIssuesByIdAsync(request.Id);
            ValidateGroupWithRequestParameters(group, request);

            return group;
        }

        private void ValidateGroupWithRequestParameters(Domain.GroupsOfIssues.GroupOfIssues group, GetGroupOfIssuesQuery request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.Id);

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.Id, request.OrganizationId);

        }
    }
}