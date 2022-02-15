using Architecture.DDD;
using Architecture.DDD.Exceptions;
using Architecture.DDD.Repositories;
using Issues.Domain.Dtos;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Issues.Domain.StatusesFlow
{
    public class StatusFlow : EntityBase, IAggregateRoot
    {
        public StatusFlow(string name, string organizationId, GroupOfIssues connectedGroupOfIssues, IEnumerable<StatusInFlowToCreateDto> statuses) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsDeleted = false;
            IsDefault = false;
            ConnectedGroupOfIssues = connectedGroupOfIssues;
            AddDefaultStatusesToFlow(statuses);
        }

        internal static StatusFlow CreateDefault(string name, string organizationId, IEnumerable<StatusInFlowToCreateDto> statuses)
        {
            var statusFlow = new StatusFlow()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                OrganizationId = organizationId,
                IsDefault = true
            };
            statusFlow.AddDefaultStatusesToFlow(statuses);
            return statusFlow;
        }

        internal static StatusFlow CreateWholeObject(string id, string name, string organizationId, bool isDefault, bool isDeleted)
        {
            return new StatusFlow()
            {
                Id = id,
                Name = name,
                OrganizationId = organizationId,
                IsDeleted = isDeleted,
                IsDefault = isDefault
            };
        }

        protected StatusFlow()
        {
            _statusesInFlow = new List<StatusInFlow>();
        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsDefault { get; protected set; }
        public GroupOfIssues ConnectedGroupOfIssues { get; protected set; }

        protected readonly List<StatusInFlow> _statusesInFlow;
        public IReadOnlyCollection<StatusInFlow> StatusesInFlow => _statusesInFlow;

        public bool IsDeleted { get; protected set; }

        public StatusInFlow AddNewStatusToFlow(string statusName)
        {
            var statusCurrentlyExistInFlow = _statusesInFlow.Any(s => string.Equals(s.Name, statusName, StringComparison.CurrentCultureIgnoreCase));
            if (statusCurrentlyExistInFlow)
                throw new DomainException(ErrorMessages.StatusWithNameIsAlreadyInFlow(statusName, Id));

            var status = new StatusInFlow(this, statusName, false);
            _statusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(StatusInFlow statusInFlowToDelete)
        {
            if (statusInFlowToDelete.StatusFlow != this)
                throw new DomainException(ErrorMessages.GivenStatusIsNotInThisFlow(statusInFlowToDelete.StatusFlow.Id, Id));

            if (statusInFlowToDelete.IsDefault)
                throw new DomainException(ErrorMessages.CouldNotDeleteDefaultStatusWithId(statusInFlowToDelete.Id));

            foreach (var statusInFlow in _statusesInFlow)
            {
                if (statusInFlow.ConnectedStatuses.Any(s => s.ConnectedStatusInFlow == statusInFlowToDelete))
                    statusInFlow.DeleteConnectedStatus(statusInFlowToDelete);
            }

            _statusesInFlow.Remove(statusInFlowToDelete);
        }

        public void ChangeDefaultStatusInFlow(StatusInFlow newDefault)
        {
            if (newDefault.StatusFlow != this)
                throw new DomainException(ErrorMessages.GivenStatusIsNotInThisFlow(newDefault.Id, Id));

            if (newDefault.IsDefault)
                throw new DomainException(ErrorMessages.GivenStatusIsAlreadyDefault(newDefault.Id));

            var currentDefault = _statusesInFlow.FirstOrDefault(s => s.IsDefault);

            //Should not happen
            if (currentDefault == null)
                throw new DomainException(ErrorMessages.ThereIsNoDefaultStatusInFlow(Id));

            currentDefault.SetDefaultToFalse();
            newDefault.SetDefaultToTrue();
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Delete()
        {
            if (IsDefault)
                throw new DomainException(ErrorMessages.DefaultStatusFlowCouldNotBeDeleted(Id));

            IsDeleted = true;
        }

        public void UnDelete()
        {
            IsDeleted = false;
        }

        private void SetDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new DefaultPropertyInStatusFlowChangedToTrueDomainEvent(this));
            ;
        }

        private void AddDefaultStatusesToFlow(IEnumerable<StatusInFlowToCreateDto> statuses)
        {
            if (!StatusInFlowToCreateDto.IsCollectionOfStatusesValid(statuses, out var reasonWhyNot))
                throw new DomainException(reasonWhyNot);
            
            _statusesInFlow.AddRange(statuses.Select(s => new StatusInFlow(this, s.StatusName, s.IsDefault)));

            foreach (var status in statuses)
            {
                if (!status.ConnectedStatuses.Any())
                    continue;

                var addedStatus = _statusesInFlow.First(s => s.Name == status.StatusName);

                foreach (var connectedStatus in status.ConnectedStatuses)
                {
                    var connectedStatusInFlow = _statusesInFlow.First(s => s.Name == connectedStatus);
                    addedStatus.AddConnectedStatus(connectedStatusInFlow);
                }
            }

        }

        public static string GetNameWithGroupOfIssues(string groupOfIssuesName) => new StringBuilder("Status flow for: ").Append(groupOfIssuesName).ToString();

        public static class ErrorMessages
        {
            public static string DefaultStatusFlowCouldNotBeDeleted(string statusFlowId) =>
                $"Default status flow with id: {statusFlowId} could not be deleted";

            public static string ThereIsNoDefaultStatusInFlow(string statusFlowId) =>
                $"There is not default status in flow with id: {statusFlowId}";

            public static string GivenStatusIsAlreadyDefault(string statusInFlowId) =>
                $"Given status with id: {statusInFlowId} is already default";

            public static string GivenStatusIsNotInThisFlow(string statusInFlowId, string flowId) =>
                $"Given status with id: {statusInFlowId} is not in the status flow with id: {flowId}";

            public static string CouldNotDeleteDefaultStatusWithId(string statusInFlowId) =>
                $"Could not delete default status with id: {statusInFlowId}";

            public static string StatusWithNameIsAlreadyInFlow(string name, string flowId) =>
                $"Status with name: {name} currently exist in flow with id: {flowId}";
        }
    }
}
