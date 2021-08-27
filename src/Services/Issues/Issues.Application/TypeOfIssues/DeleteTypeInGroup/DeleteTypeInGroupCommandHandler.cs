using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.DeleteTypeInGroup
{
    public class DeleteTypeInGroupCommandHandler : IRequestHandler<DeleteTypeInGroupCommand>
    {
        private readonly ITypeOfIssueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTypeInGroupCommandHandler(ITypeOfIssueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteTypeInGroupCommand request, CancellationToken cancellationToken)
        {
            var allTypes = await _repository.GetTypeOfIssuesForOrganizationAsync(request.OrganizationId);

            var requestedType = allTypes.FirstOrDefault(s =>
                s.Id == request.TypeOfIssueId &&
                s.TypesInGroups.FirstOrDefault()?.TypeOfGroupOfIssuesId == request.TypeOfGroupOfIssuesId);
    
            ValidateTypeWithRequestedParameters(requestedType, request);

            requestedType.DeleteTypeOfGroup(request.TypeOfGroupOfIssuesId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, DeleteTypeInGroupCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssueId} and type of group of issues id: {request.TypeOfGroupOfIssuesId} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssueId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issue with id: {request.TypeOfIssueId} is already archived");
        }
    }
}