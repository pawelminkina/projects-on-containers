﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.CQRS.GroupOfIssues.Commands.ChangeShortNameInGroup
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

            requestedGroup.ChangeShortName(request.NewShortName);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeWithRequestedParameters(Domain.GroupsOfIssues.GroupOfIssues group, ChangeShortNameInGroupOfIssuesCommand request)
        {
            if (group is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.Id);
            if (group.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(request.Id, request.OrganizationId);
        }
    }
}
