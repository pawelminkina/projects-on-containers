using System;
using System.Formats.Asn1;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.GroupOfIssues.Commands.CreateGroup
{
    public class CreateGroupOfIssuesCommand : IRequest<string>
    {

        public CreateGroupOfIssuesCommand(string typeOfGroupId, string name, string shortName, string organizationId)
        {
            TypeOfGroupId = typeOfGroupId;
            Name = name;
            OrganizationId = organizationId;
            ShortName = shortName;
        }

        public string TypeOfGroupId { get; }
        public string Name { get; }
        public string ShortName { get; }
        public string OrganizationId { get; }
    }

    public class CreateGroupOfIssuesCommandHandler : IRequestHandler<CreateGroupOfIssuesCommand, string>
    {
        private readonly ITypeOfGroupOfIssuesRepository _repository;
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IGroupOfIssuesRepository groupOfIssuesRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.TypeOfGroupId);
            ValidateTypeWithRequestedParameters(type, request);

            var group = type.AddNewGroupOfIssues(request.Name, request.ShortName);
            
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return group.Id;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, CreateGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of group of issue with id: {request.TypeOfGroupId} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issue with id: {request.TypeOfGroupId} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}