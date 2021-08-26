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
            if (type is null)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found is not accessible for organization with id: {request.OrganizationId}");

            return type;
        }
    }
}