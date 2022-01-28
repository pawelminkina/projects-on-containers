using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.CQRS.TypeOfGroupOfIssues.Queries.GetType
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
            if (type is null)
                throw NotFoundException.RequestedResourceWithIdDoWasNotFound(request.Id);

            if (type.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.Id, request.OrganizationId);

        }
    }
}