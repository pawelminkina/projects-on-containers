using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.GetType
{
    public class GetTypeOfGroupOfIssuesQueryHandler : IRequestHandler<GetTypeOfGroupOfIssuesQuery, Domain.GroupsOfIssues.TypeOfGroupOfIssues>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;

        public GetTypeOfGroupOfIssuesQueryHandler(ITypeOfGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Domain.GroupsOfIssues.TypeOfGroupOfIssues> Handle(GetTypeOfGroupOfIssuesQuery request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.Id);

            if (type is not null)
                ValidateTypeWithRequestParameters(type, request);

            return type;
        }

        private void ValidateTypeWithRequestParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, GetTypeOfGroupOfIssuesQuery request)
        {
            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");

        }
    }
}