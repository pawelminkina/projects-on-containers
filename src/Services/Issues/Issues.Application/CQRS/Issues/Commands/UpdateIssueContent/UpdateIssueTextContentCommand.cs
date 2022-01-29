using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.CQRS.Issues.Commands.UpdateIssueContent
{
    public class UpdateIssueTextContentCommand : IRequest
    {
        public UpdateIssueTextContentCommand(string issueId, string textContent, string organizationId)
        {
            IssueId = issueId;
            TextContent = textContent;
            OrganizationId = organizationId;
        }
        public string IssueId { get; }
        public string TextContent { get; }
        public string OrganizationId { get; }
    }
    public class UpdateIssueTextContentCommandHandler : IRequestHandler<UpdateIssueTextContentCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIssueTextContentCommandHandler(IIssueRepository issueRepository, IUnitOfWork unitOfWork)
        {
            _issueRepository = issueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateIssueTextContentCommand request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
            ValidateIssueWithRequestedParameters(issue, request);

            issue.ChangeTextContent(request.TextContent);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateIssueWithRequestedParameters(Domain.Issues.Issue issue, UpdateIssueTextContentCommand request)
        {
            if (issue is null)
                throw NotFoundException.RequestedResourceWithIdDoWasNotFound(request.IssueId);

            if (issue.GroupOfIssue.TypeOfGroup.OrganizationId != request.OrganizationId)
                throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(issue.GroupOfIssue.TypeOfGroup.OrganizationId, request.OrganizationId);
        }
    }
}