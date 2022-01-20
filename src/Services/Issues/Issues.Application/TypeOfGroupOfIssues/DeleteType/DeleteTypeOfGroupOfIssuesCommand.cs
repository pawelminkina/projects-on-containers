using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.DeleteType
{
    public class DeleteTypeOfGroupOfIssuesCommand : IRequest
    {
        public DeleteTypeOfGroupOfIssuesCommand(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }
    }
    public class DeleteTypeOfGroupOfIssuesCommandHandler : IRequestHandler<DeleteTypeOfGroupOfIssuesCommand>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.Id);
            ValidateTypeWithRequestedParameters(type, request);

            if (type.CanBeDeleted(out var reasonWhyNot))
                throw new InvalidOperationException($"Delete operation failed reason: {reasonWhyNot}");
            
            await _repository.DeleteTypeofGroupOfIssuesAsync(type.Id);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, DeleteTypeOfGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was found and is not accessible for organization with id: {request.OrganizationId}"); }
    }
}