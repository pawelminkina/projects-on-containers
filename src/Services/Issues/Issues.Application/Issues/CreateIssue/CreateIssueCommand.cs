using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.CreateIssue
{
    public class CreateIssueCommand : IRequest<string>
    {
        public CreateIssueCommand(string name, string groupId, string textContent, string userId, string organizationId, string typeOfIssueId)
        {
            Name = name;
            GroupId = groupId;
            TextContent = textContent;
            UserId = userId;
            OrganizationId = organizationId;
            TypeOfIssueId = typeOfIssueId;
        }
        public string Name { get; }
        public string GroupId { get; }
        public string TextContent { get; }
        public string UserId { get; }
        public string OrganizationId { get; }
        public string TypeOfIssueId { get; }
    }
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, string>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIssueCommandHandler(IGroupOfIssuesRepository groupOfIssuesRepository, IUnitOfWork unitOfWork)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.GroupId);
            ValidateGroupWithRequestedParameters(group, request);

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

            if (group.IsDeleted)
                throw new InvalidOperationException($"Group of issue with id: {request.GroupId} is deleted");
        }
    }
}