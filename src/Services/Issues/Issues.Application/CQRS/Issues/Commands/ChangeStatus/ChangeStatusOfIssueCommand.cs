using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using Architecture.DDD.Repositories;
using Issues.Application.Common.Exceptions;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using MediatR;

namespace Issues.Application.CQRS.Issues.Commands.ChangeStatus;

public class ChangeStatusOfIssueCommand : IRequest
{
    public string IssueId { get; }
    public string NewStatusInFlowId { get; }
    public string OrganizationId { get; }

    public ChangeStatusOfIssueCommand(string issueId, string newStatusInFlowId, string organizationId)
    {
        IssueId = issueId;
        NewStatusInFlowId = newStatusInFlowId;
        OrganizationId = organizationId;
    }
}

public class ChangeStatusOfIssueCommandHandler : IRequestHandler<ChangeStatusOfIssueCommand>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IStatusFlowRepository _statusFlowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStatusOfIssueCommandHandler(IIssueRepository issueRepository, IStatusFlowRepository statusFlowRepository, IUnitOfWork unitOfWork)
    {
        _issueRepository = issueRepository;
        _statusFlowRepository = statusFlowRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(ChangeStatusOfIssueCommand request, CancellationToken cancellationToken)
    {
        var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId);
        ValidateIssueWithRequestedParameters(issue, request);

        var newStatus = await _statusFlowRepository.GetStatusInFlowById(request.NewStatusInFlowId);
        if (newStatus is null)
            throw NotFoundException.RequestedResourceWithIdWasNotFound(request.NewStatusInFlowId);

        issue.ChangeStatusInFlow(newStatus);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }

    private void ValidateIssueWithRequestedParameters(Domain.Issues.Issue issue, ChangeStatusOfIssueCommand request)
    {
        if (issue is null)
            throw NotFoundException.RequestedResourceWithIdWasNotFound(request.IssueId);

        if (issue.GroupOfIssue.TypeOfGroup.OrganizationId != request.OrganizationId)
            throw PermissionDeniedException.ResourceFoundAndNotAccessibleInOrganization(issue.GroupOfIssue.TypeOfGroup.OrganizationId, request.OrganizationId);
    }
}