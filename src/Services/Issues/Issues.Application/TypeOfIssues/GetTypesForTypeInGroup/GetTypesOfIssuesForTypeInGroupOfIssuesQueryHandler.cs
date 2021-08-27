using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetTypesForTypeInGroup
{
    public class GetTypesOfIssuesForTypeInGroupOfIssuesQueryHandler : IRequestHandler<GetTypesOfIssuesForTypeInGroupOfIssuesQuery, IEnumerable<TypeOfIssue>>
    {
        private readonly ITypeOfIssueRepository _repository;

        public GetTypesOfIssuesForTypeInGroupOfIssuesQueryHandler(ITypeOfIssueRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TypeOfIssue>> Handle(GetTypesOfIssuesForTypeInGroupOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var allTypes = await _repository.GetTypeOfIssuesForOrganizationAsync(request.OrganizationId);

            return allTypes.Where(s=>!s.IsArchived && s.TypesInGroups.FirstOrDefault()?.TypeOfGroupOfIssuesId == request.TypeOfGroupOfIssuesId);
        }
    }
}