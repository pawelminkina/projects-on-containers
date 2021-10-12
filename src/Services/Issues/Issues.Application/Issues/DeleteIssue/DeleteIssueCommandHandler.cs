using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.DeleteIssue
{
    public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIssueCommandHandler(IIssueRepository issueRepository, IUnitOfWork unitOfWork)
        {
            _issueRepository = issueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
            ValidateIssueWithRequestedParameters(issue, request);

            issue.Delete();
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }

        private void ValidateIssueWithRequestedParameters(Issue issue, DeleteIssueCommand request)
        {
            if (issue is null)
                throw new InvalidOperationException($"Issue with given id: {request.IssueId} does not exist");

            if (issue.IsArchived)
                throw new InvalidOperationException($"Issue with given id: {request.IssueId} is archived and cannot be used");

            if (issue.GroupOfIssue.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Issue with given id: {request.IssueId} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}
