using Architecture.DDD;
using Issues.Domain.StatusesFlow.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.DDD.Exceptions;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlow : EntityBase
    {
        internal StatusInFlow(StatusFlow statusFlow, string name, bool isDefault) : this()
        {
            Id = Guid.NewGuid().ToString();
            StatusFlow = statusFlow;
            Name = name;
            IsDefault = isDefault;
        }
        protected StatusInFlow()
        {
            _connectedStatuses = new List<StatusInFlowConnection>();
        }

        internal static StatusInFlow CreateWholeObject(string id, string statusFlowId, string name, bool isDefault)
        {
            return new StatusInFlow()
            {
                Id = id,
                _statusFlowId = statusFlowId,
                Name = name,
                IsDefault = isDefault
            };
        }

        public StatusFlow StatusFlow { get; protected set; }
        private string _statusFlowId;
        public string Name { get; protected set; }
        public bool IsDefault { get; protected set; }

        protected readonly List<StatusInFlowConnection> _connectedStatuses;
        public IReadOnlyCollection<StatusInFlowConnection> ConnectedStatuses => _connectedStatuses;

        public void AddConnectedStatus(StatusInFlow status)
        {
            if (_connectedStatuses.Any(s => s.ConnectedStatusInFlow == status))
                throw new DomainException(ErrorMessages.StatusIsAlreadyConnectedToParentStatus(status.Id, Id));

            if (status.StatusFlow != StatusFlow)
                throw new DomainException(ErrorMessages.GivenStatusToConnectIsInDifferentStatusFlow(status.StatusFlow.Id, _statusFlowId));

            var connectedStatus = new StatusInFlowConnection(this, status);
            _connectedStatuses.Add(connectedStatus);
        }

        public void DeleteConnectedStatus(StatusInFlow status)
        {
            var connectionToDelete = _connectedStatuses.FirstOrDefault(s => s.ConnectedStatusInFlow == status);
            if (connectionToDelete is null)
                throw new DomainException(ErrorMessages.ConnectionBetweenStatusesDoNotExist(Id, status.Id));

            _connectedStatuses.Remove(connectionToDelete);

        }

        public bool IsConnectedTo(StatusInFlow statusInFlow)
        {
            return _connectedStatuses.Any(s => s.ConnectedStatusInFlow == statusInFlow);
        }

        public void SetDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new StatusInFlowDefaultPropertyChangedToTrueDomainEvent(this));
        }

        public void SetDefaultToFalse()
        {
            IsDefault = false;
            AddDomainEvent(new StatusInFlowDefaultPropertyChangedToFalseDomainEvent(this));
        }

        public static class ErrorMessages
        {
            public static string StatusIsAlreadyConnectedToParentStatus(string connectedStatusId, string parentStatusInFlowId) =>
                $"Status with connectedStatusId: {connectedStatusId} is already added to connected statuses where status in flow has id: {parentStatusInFlowId}";

            public static string GivenStatusToConnectIsInDifferentStatusFlow(string givenStatusFlowId, string currentStatusFlowId) =>
                $"Given status in flow to connect is in different status flow with id: {givenStatusFlowId} then parent status flow id: {currentStatusFlowId}";

            public static string ConnectionBetweenStatusesDoNotExist(string parentStatusId, string connectedStatusId) =>
                $"There is no connection between parent status with id: {parentStatusId} and connected with id: {connectedStatusId}";
        }
    }
}
