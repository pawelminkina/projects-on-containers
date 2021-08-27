using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetTypes
{
    public class GetTypesOfIssuesQueryHandler : IRequestHandler<GetTypesOfIssuesQuery, IEnumerable<TypeOfIssue>>
    {
        private readonly ITypeOfIssueRepository _repository;

        public GetTypesOfIssuesQueryHandler(ITypeOfIssueRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TypeOfIssue>> Handle(GetTypesOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var allTypes = await _repository.GetTypeOfIssuesForOrganizationAsync(request.OrganizationId);

            return allTypes.Where(s => !s.IsArchived);
        }
    }
}