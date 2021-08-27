using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.ArchiveType
{
    public class ArchiveTypeOfIssuesCommandHandler : IRequestHandler<ArchiveTypeOfIssuesCommand>
    {
        private readonly ITypeOfIssueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveTypeOfIssuesCommandHandler(ITypeOfIssueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveTypeOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfIssueByIdAsync(request.TypeOfIssuesId);

            ValidateTypeWithRequestedParameters(type, request);

            type.Archive();
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, ArchiveTypeOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssuesId} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssuesId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssuesId} is already archived");
        }
    }
}