using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.ArchiveIssue
{
    public class ArchiveIssueCommandHandler : IRequestHandler<ArchiveIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveIssueCommandHandler(IIssueRepository issueRepository, IUnitOfWork unitOfWork)
        {
            _issueRepository = issueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ArchiveIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
            ValidateIssueWithRequestedParameters(issue, request);

            issue.Archive();
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateIssueWithRequestedParameters(Domain.Issues.Issue issue, ArchiveIssueCommand request)
        {
            if (issue is null)
                throw new InvalidOperationException($"Issue with id: {request.IssueId} was not found");

            if (issue.TypeOfIssue.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Issue with id: {request.IssueId} was found and is not accessible for organization with id: {request.OrganizationId}");

            if (issue.IsArchived)
                throw new InvalidOperationException($"Issue with id: {request.IssueId} is already archived");
        }
    }
}