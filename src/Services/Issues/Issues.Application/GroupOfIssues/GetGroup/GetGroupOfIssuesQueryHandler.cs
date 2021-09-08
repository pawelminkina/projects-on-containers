using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.GroupOfIssues.GetGroup
{
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
            if (group is not null)
                ValidateGroupWithRequestParameters(group, request);

            return group;
        }

        private void ValidateGroupWithRequestParameters(Domain.GroupsOfIssues.GroupOfIssues group, GetGroupOfIssuesQuery request)
        {
            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");

        }
    }
}