using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.GetIssuesForGroup
{
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

            return group.Issues.Where(d=> !d.IsDeleted);
        }

        private void ValidateGroupWithRequestParameters(Domain.GroupsOfIssues.GroupOfIssues group, GetIssuesForGroupQuery request)
        {
            if (group is null)
                throw new InvalidOperationException($"Group of issues with id: {request.GroupId} was not found");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issues with id: {request.GroupId} was found and is not accessible for organization with id: {request.OrganizationId}");

        }
    }
}