using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.RenameIssue
{
    public class RenameIssueCommandHandler : IRequestHandler<RenameIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenameIssueCommandHandler(IIssueRepository issueRepository, IUnitOfWork unitOfWork)
        {
            _issueRepository = issueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RenameIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
            ValidateIssueWithRequestedParameters(issue,request);

            issue.Rename(request.NewName);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateIssueWithRequestedParameters(Domain.Issues.Issue issue, RenameIssueCommand request)
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