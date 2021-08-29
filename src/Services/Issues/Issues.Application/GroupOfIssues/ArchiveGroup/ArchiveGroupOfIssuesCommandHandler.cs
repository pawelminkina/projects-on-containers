using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Issues.Application.GroupOfIssues.ArchiveGroup
{
    public class ArchiveGroupOfIssuesCommandHandler : IRequestHandler<ArchiveGroupOfIssuesCommand>
    {
        private readonly IGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveGroupOfIssuesCommandHandler(IGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetGroupOfIssuesByIdAsync(request.GroupOfIssuesId);
            
            ValidateTypeWithRequestedParameters(group,request);

            group.Archive();
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, ArchiveGroupOfIssuesCommand request)
        {
            if (group is null)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupOfIssuesId} was not found");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupOfIssuesId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (group.IsArchived)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupOfIssuesId} is already archived");
        }
    }
}