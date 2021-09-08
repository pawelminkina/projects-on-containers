using System;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetType
{
    public class GetTypeOfIssueQueryHandler : IRequestHandler<GetTypeOfIssueQuery, TypeOfIssue>
    {
        private readonly ITypeOfIssueRepository _repository;

        public GetTypeOfIssueQueryHandler(ITypeOfIssueRepository repository)
        {
            _repository = repository;
        }
        public async Task<TypeOfIssue> Handle(GetTypeOfIssueQuery request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfIssueByIdAsync(request.Id);
            if (type is not null)
                ValidateTypeWithRequestedParameters(type, request);

            return type;
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, GetTypeOfIssueQuery request)
        {
            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}