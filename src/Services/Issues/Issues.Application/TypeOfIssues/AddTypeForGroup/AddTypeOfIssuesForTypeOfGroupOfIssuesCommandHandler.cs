using System;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.AddTypeForGroup
{
    public class AddTypeOfIssuesForTypeOfGroupOfIssuesCommandHandler : IRequestHandler<AddTypeOfIssuesForTypeOfGroupOfIssuesCommand>
    {
        private readonly ITypeOfIssueRepository _typeOfIssueRepository;
        private readonly ITypeOfGroupOfIssuesRepository _typeOfGroupOfIssuesRepository;
        private readonly IStatusFlowRepository _statusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTypeOfIssuesForTypeOfGroupOfIssuesCommandHandler(ITypeOfIssueRepository typeOfIssueRepository, ITypeOfGroupOfIssuesRepository typeOfGroupOfIssuesRepository, IStatusFlowRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _typeOfIssueRepository = typeOfIssueRepository;
            _typeOfGroupOfIssuesRepository = typeOfGroupOfIssuesRepository;
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddTypeOfIssuesForTypeOfGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            var type = await _typeOfIssueRepository.GetTypeOfIssueByIdAsync(request.TypeOfIssuesId);
            ValidateTypeOfIssueWithRequestParameters(type, request);

            var typeOfGroup = await _typeOfGroupOfIssuesRepository.GetTypeOfGroupOfIssuesByIdAsync(request.TypeOfGroupOfIssuesId);
            ValidateTypeGroupOfOfIssueWithRequestParameters(typeOfGroup, request);

            var statusFlow = await _statusRepository.GetFlowById(request.StatusFlowId);
            ValidateStatusFlowWithRequestParameters(statusFlow, request);

            type.AddNewTypeOfGroupToCollection(request.TypeOfGroupOfIssuesId, request.StatusFlowId);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidateTypeOfIssueWithRequestParameters(TypeOfIssue type, AddTypeOfIssuesForTypeOfGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Requested type of issue with id: {request.TypeOfIssuesId} does not exist");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeOfIssuesId} is archived and cannot be modified modified and used");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of issues with given id: {request.TypeOfIssuesId} is not assigned to organization with id: {request.OrganizationId}");

        }

        private void ValidateTypeGroupOfOfIssueWithRequestParameters(Domain.GroupsOfIssues.TypeOfGroupOfIssues type, AddTypeOfIssuesForTypeOfGroupOfIssuesCommand request)
        {
            if (type is null)
                throw new InvalidOperationException($"Requested type of group of issue with id: {request.TypeOfGroupOfIssuesId} does not exist");

            if (type.IsArchived)
                throw new InvalidOperationException($"Type of group of issue with given id: {request.TypeOfGroupOfIssuesId} is archived and cannot be modified modified and used");

            if (type.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Type of group of issue with given id: {request.TypeOfGroupOfIssuesId} is not assigned to organization with id: {request.OrganizationId}");
        }

        private void ValidateStatusFlowWithRequestParameters(Domain.StatusesFlow.StatusFlow flow, AddTypeOfIssuesForTypeOfGroupOfIssuesCommand request)
        {
            if (flow is null)
                throw new InvalidOperationException($"Requested status flow with id: {request.StatusFlowId} does not exist");

            if (flow.IsArchived)
                throw new InvalidOperationException($"Status flow with given id: {request.StatusFlowId} is archived and cannot be modified and used");

            if (flow.OrganizationId != request.OrganizationId)
                throw new InvalidOperationException($"Status flow with given id: {request.StatusFlowId} is not assigned to organization with id: {request.OrganizationId}");
        }
    }
}