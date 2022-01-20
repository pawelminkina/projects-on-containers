using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.CreateType
{
    public class CreateTypeOfGroupOfIssuesCommand : IRequest<string>
    {
        public CreateTypeOfGroupOfIssuesCommand(string name, string organizationId)
        {
            Name = name;
            OrganizationId = organizationId;
        }

        public string Name { get; }
        public string OrganizationId { get; }
    }

    public class CreateTypeOfGroupOfIssuesCommandHandler : IRequestHandler<CreateTypeOfGroupOfIssuesCommand, string>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = new Domain.GroupsOfIssues.TypeOfGroupOfIssues(request.OrganizationId, request.Name);

            await _repository.AddNewTypeofGroupOfIssuesAsync(type);

            await _unitOfWork.CommitAsync(cancellationToken);

            return type.Id;
        }

    }
}