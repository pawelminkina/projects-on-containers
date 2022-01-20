using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.RenameType
{
    public class RenameTypeOfGroupOfIssuesCommand : IRequest
    {
        public RenameTypeOfGroupOfIssuesCommand(string id, string organizationId, string newName)
        {
            Id = id;
            OrganizationId = organizationId;
            NewName = newName;
        }

        public string Id { get; }
        public string OrganizationId { get; }
        public string NewName { get; }
    }
    public class RenameTypeOfGroupOfIssuesCommandHandler : IRequestHandler<RenameTypeOfGroupOfIssuesCommand>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RenameTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RenameTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.Id);
            ValidateTypeWithRequestedParameters(type,request);

            type.RenameTypeOfGroup(request.NewName);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;

        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, RenameTypeOfGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}