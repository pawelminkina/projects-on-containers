﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.StatusFlow.Queries.GetStatusFlowForGroup
{
    public class GetStatusFlowForGroupOfIssuesQuery : IRequest<Domain.StatusesFlow.StatusFlow>
    {
        public string GroupOfIssuesId { get; }
        public string OrganizationId { get; }

        public GetStatusFlowForGroupOfIssuesQuery(string groupOfIssuesId, string organizationId)
        {
            GroupOfIssuesId = groupOfIssuesId;
            OrganizationId = organizationId;
        }
    }

    public class GetStatusFlowForGroupOfIssuesQueryHandler : IRequestHandler<GetStatusFlowForGroupOfIssuesQuery, Domain.StatusesFlow.StatusFlow>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;

        public GetStatusFlowForGroupOfIssuesQueryHandler(IGroupOfIssuesRepository groupOfIssuesRepository)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
        }
        public async Task<Domain.StatusesFlow.StatusFlow> Handle(GetStatusFlowForGroupOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var groupOfIssues = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.GroupOfIssuesId);
            ValidateGroupWithRequestParameters(groupOfIssues, request);

            return groupOfIssues.ConnectedStatusFlow;
            
        }

        private void ValidateGroupWithRequestParameters(Domain.GroupsOfIssues.GroupOfIssues group, GetStatusFlowForGroupOfIssuesQuery request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.GroupOfIssuesId);

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.GroupOfIssuesId, request.OrganizationId);
        }
    }
}
