using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.GetType
{
    public class GetTypeOfGroupOfIssuesQuery : IRequest<Domain.GroupsOfIssues.TypeOfGroupOfIssues>
    {
        public GetTypeOfGroupOfIssuesQuery(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }

    }
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
            ValidateTypeWithRequestParameters(type, request);

            return type;
        }

        private void ValidateTypeWithRequestParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, GetTypeOfGroupOfIssuesQuery request)
        {
            //TODO 404
            if (type is null)
                throw new InvalidOperationException($"Requested type with id: {request.Id} don't exist");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");

        }
    }
}