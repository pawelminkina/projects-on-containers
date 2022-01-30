using Architecture.DDD;
using Issues.Domain.StatusesFlow.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ConnectedStatuses = new List<StatusInFlowConnection>();
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

        public List<StatusInFlowConnection> ConnectedStatuses { get; private set; } //dunno how i will do this xD, functional tests will show

        public void AddConnectedStatus(StatusInFlow status)
        {
            if (status == null)
                throw new InvalidOperationException("Given status to add is null");

            if (ConnectedStatuses.Any(s => s.ConnectedStatusInFlow.Id == status.Id))
                throw new InvalidOperationException(
                    $"Status with connectedStatusId: {status.Id} is already added to connected statuses where status in flow has connectedStatusId: {Id}");

            if (status.StatusFlow.Id != StatusFlow.Id)
                throw new InvalidOperationException("Given status in flow to connect is in different status flow");

            var connectedStatus = new StatusInFlowConnection(this, status);
            ConnectedStatuses.Add(connectedStatus);
        }

        public void DeleteConnectedStatus(StatusInFlow status)
        {
            if (status is null)
                throw new InvalidOperationException("Given status to delete is null");

            var connectionToDelete = ConnectedStatuses.FirstOrDefault(s => s.ConnectedStatusInFlow.Id == status.Id);
            if (connectionToDelete is null)
                throw new InvalidOperationException($"There is no connection between parent status with id: {Id} and connected with id: {status.Id}");
            
            ConnectedStatuses.Remove(connectionToDelete);

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
    }
}
