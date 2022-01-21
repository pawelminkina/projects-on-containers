using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.CQRS.TypeOfGroupOfIssues.Queries.GetTypes
{
    public class GetTypesOfGroupOfIssuesQuery : IRequest<IEnumerable<Domain.GroupsOfIssues.TypeOfGroupOfIssues>>
    {
        public GetTypesOfGroupOfIssuesQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }

    }

    public class GetTypesOfGroupOfIssuesQueryHandler : IRequestHandler<GetTypesOfGroupOfIssuesQuery, IEnumerable<Domain.GroupsOfIssues.TypeOfGroupOfIssues>>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public GetTypesOfGroupOfIssuesQueryHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Domain.GroupsOfIssues.TypeOfGroupOfIssues>> Handle(GetTypesOfGroupOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(request.OrganizationId);
            return types;
        }
    }
}