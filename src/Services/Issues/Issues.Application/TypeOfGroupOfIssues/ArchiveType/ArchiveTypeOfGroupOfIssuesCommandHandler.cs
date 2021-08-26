﻿using System;
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
        private readonly ITypeGroupOfIssuesArchivePolicy _archivePolicy;

        public ArchiveTypeOfGroupOfIssuesCommandHandler(ITypeOfGroupOfIssuesRepository repository, IUnitOfWork unitOfWork, ITypeGroupOfIssuesArchivePolicy archivePolicy)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _archivePolicy = archivePolicy;
        }
        public async Task<Unit> Handle(ArchiveTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _repository.GetTypeOfGroupOfIssuesByIdAsync(request.Id);
            if (type is null)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issues with id: {request.Id} was not found is not accessible for organization with id: {request.OrganizationId}");

            type.Archive(_archivePolicy);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}