using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.ArchiveType
{
    public class ArchiveTypeOfGroupOfIssuesCommandHandler : IRequestHandler<ArchiveTypeOfGroupOfIssuesCommand>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.Id);
            ValidateTypeWithRequestedParameters(type, request);

            Domain.GroupsOfIssues.TypeOfGroupOfIssues typeWhereGroupWillBeMoved = null;
            if (string.IsNullOrWhiteSpace(request.TypeOfGroupOfIssuesWhereGroupsWillBeMovedId))
                typeWhereGroupWillBeMoved =
                    (await _repository.GetTypeOfGroupOfIssuesForOrganizationAsync(request.OrganizationId))
                    .FirstOrDefault(d => d.IsDefault);
            else
                typeWhereGroupWillBeMoved =
                    await _repository.GetTypeOfGroupOfIssuesByIdAsync(request
                        .TypeOfGroupOfIssuesWhereGroupsWillBeMovedId);

            if (typeWhereGroupWillBeMoved is null)
                throw new InvalidOperationException("Type to move groups was not found");

            type.ArchiveAndMoveGroups(typeWhereGroupWillBeMoved);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, ArchiveTypeOfGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (type.IsDefault)
                throw new InvalidOperationException("You can't archive an type which is default");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} is already archived");
        }
    }
}