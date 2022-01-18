using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.GroupOfIssues.ChangeShortNameInGroup
{
    public class ChangeShortNameInGroupOfIssuesCommand : IRequest
    {
        public string Id { get; }
        public string NewShortName { get; }
        public string OrganizationId { get; }

        public ChangeShortNameInGroupOfIssuesCommand(string id, string newShortName, string organizationId)
        {
            Id = id;
            NewShortName = newShortName;
            OrganizationId = organizationId;
        }
    }

    public class ChangeShortNameInGroupOfIssuesCommandHandler : IRequestHandler<ChangeShortNameInGroupOfIssuesCommand>
    {
        private readonly IGroupOfIssuesRepository _groupOfIssuesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeShortNameInGroupOfIssuesCommandHandler(IGroupOfIssuesRepository groupOfIssuesRepository, IUnitOfWork unitOfWork)
        {
            _groupOfIssuesRepository = groupOfIssuesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangeShortNameInGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var requestedGroup = await _groupOfIssuesRepository.GetGroupOfIssuesByIdAsync(request.Id);
            ValidateTypeWithRequestedParameters(requestedGroup, request);

            if (await _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync(request.NewShortName, request.OrganizationId))
                throw new InvalidOperationException("Group with requested short name already exist");

            requestedGroup.ChangeShortName(requestedGroup.ShortName);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, ChangeShortNameInGroupOfIssuesCommand request)
        {
            //TODO custom exceptions which return status codes for example there 404
            if (group is null)
                throw new InvalidOperationException("Requested group was not found");

            if (group.IsDeleted)
                throw new InvalidOperationException($"Cannot change short name of group with id: {request.Id} which is deleted");

            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Group of issue with id: {request.OrganizationId} was found and is not accessible for organization with id: {request.OrganizationId}");
        }
    }
}
