using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.GroupOfIssues.GetGroupsForOrganization
{
    public class GetGroupsOfIssuesForOrganizationQueryHandler : IRequestHandler<GetGroupsOfIssuesForOrganizationQuery, IEnumerable<Domain.GroupsOfIssues.GroupOfIssues>>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public GetGroupsOfIssuesForOrganizationQueryHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.GroupsOfIssues.GroupOfIssues>> Handle(GetGroupsOfIssuesForOrganizationQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(request.OrganizationId);
            return types.SelectMany(s => s.Groups).Distinct();
        }
    }
}