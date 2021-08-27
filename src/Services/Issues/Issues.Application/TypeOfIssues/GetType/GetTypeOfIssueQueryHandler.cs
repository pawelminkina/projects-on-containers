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

            ValidateTypeWithRequestedParameters(type, request);

            return type;
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, GetTypeOfIssueQuery request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of issues with given id: {request.Id} does not exist");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issues with given id: {request.Id} is archived and cannot be modified");
        }
    }
}