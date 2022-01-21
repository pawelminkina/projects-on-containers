using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.CQRS.Issues.Queries.GetIssuesForGroup
{
    public class GetIssuesForGroupQuery : IRequest<IEnumerable<Issue>>
    {
        public GetIssuesForGroupQuery(string groupId, string organizationId)
        {
            GroupId = groupId;
            OrganizationId = organizationId;
        }
        public string GroupId { get; }
        public string OrganizationId { get; }

    }
    
    public class GetIssuesForGroupQueryHandler : IRequestHandler<GetIssuesForGroupQuery, IEnumerable<Issue>>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;

        public GetIssuesForGroupQueryHandler(IGroupOfIssuesRepository groupOfIssuesRepository)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
        }
        public async Task<IEnumerable<Issue>> Handle(GetIssuesForGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.GroupId);
            ValidateGroupWithRequestParameters(group, request);

            return group.Issues;
        }

        private void ValidateGroupWithRequestParameters(Domain.GroupsOfIssues.GroupOfIssues group, GetIssuesForGroupQuery request)
        {
            if (group is null)
                throw new InvalidOperationException($"Group of issues with id: {request.GroupId} was not found");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issues with id: {request.GroupId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (group.IsDeleted)
                throw new InvalidOperationException($"Requested group with id: {request.GroupId} is deleted");
        }
    }
}