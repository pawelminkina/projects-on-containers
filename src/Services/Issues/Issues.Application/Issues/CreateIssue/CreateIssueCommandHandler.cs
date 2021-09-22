using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.Issues.CreateIssue
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, string>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly ITypeOfIssueRepository _typeOfIssueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIssueCommandHandler(IGroupOfIssuesRepository groupOfIssuesRepository, ITypeOfIssueRepository typeOfIssueRepository, IUnitOfWork unitOfWork)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _typeOfIssueRepository = typeOfIssueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.GroupId);
            ValidateGroupWithRequestedParameters(group, request);

            var type = await _typeOfIssueRepository.GetTypeOfIssueByIdAsync(request.TypeOfIssueId);
            ValidateTypeWithRequestedParameters(type,request);

            var typeInGroup = type.TypesInGroups.FirstOrDefault(s => s.TypeOfGroupOfIssuesId == group.Id);
            if (typeInGroup is null)
                throw new InvalidOperationException($"Group for type {request.GroupId} was not found in type of issue: {request.TypeOfIssueId}");

            var issue = group.AddIssue(request.Name, request.UserId, request.TextContent, request.TypeOfIssueId, typeInGroup.Flow.StatusesInFlow.FirstOrDefault(d=>d.IndexInFlow == 0).ParentStatusId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return issue.Id;
        }

        private void ValidateGroupWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, CreateIssueCommand request)
        {
            if (group is null)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} was not found");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (group.IsArchived)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} is already archived");
        }

        private void ValidateTypeWithRequestedParameters(TypeOfIssue type, CreateIssueCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeOfIssueId} does not exist");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeOfIssueId} is archived and cannot be used");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeOfIssueId} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}