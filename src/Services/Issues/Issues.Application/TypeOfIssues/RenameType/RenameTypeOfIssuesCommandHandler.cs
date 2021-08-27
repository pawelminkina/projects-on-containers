using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.RenameType
{
    public class RenameTypeOfIssuesCommandHandler : IRequestHandler<RenameTypeOfIssuesCommand>
    {
        private readonly ITypeOfIssueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RenameTypeOfIssuesCommandHandler(ITypeOfIssueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RenameTypeOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfIssueByIdAsync(request.TypeIfIssueId);
            
            ValidateTypeWithRequestedParameters(type, request);

            type.Rename(request.NewName);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, RenameTypeOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeIfIssueId} does not exist");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeIfIssueId} is archived and cannot be modified");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeIfIssueId} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}