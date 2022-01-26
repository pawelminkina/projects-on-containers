using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow.DomainEvents;

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

        internal static StatusInFlow CreateWholeObject(string id, string statusFlowId, string name, string isDefault)
        {
            return null;
        }

        public StatusFlow StatusFlow { get; protected set; }
        private string _statusFlowId;
        public string Name { get; protected set; }
        public bool IsDefault { get; protected set; }

        public List<StatusInFlowConnection> ConnectedStatuses { get; private set; } //dunno how i will do this xD, functional tests will show

        public void AddConnectedStatus(StatusInFlow status, StatusInFlowDirection direction)
        {
            if (status == null)
                throw new InvalidOperationException("Given status to add is null");

            if (ConnectedStatuses.Any(s=> s.ConnectedStatusInFlow.Id == status.Id && s.Direction == direction))
                throw new InvalidOperationException(
                    $"Status with connectedStatusId: {status.Id} is already added to connected statuses where status in flow has connectedStatusId: {Id} with direction of connection: {direction.ToString()}");

            if (status.StatusFlow.Id != StatusFlow.Id)
                throw new InvalidOperationException("Given status in flow to connect is in different status flow");

            var connectedStatus = new StatusInFlowConnection(this, direction, status);
            ConnectedStatuses.Add(connectedStatus);
            AddDomainEvent(new ConnectedStatusAddedDomainEvent(connectedStatus));
        }

        public void DeleteConnectedStatus(StatusInFlow status, params StatusInFlowDirection[] directions)
        {
            if (status is null)
                throw new InvalidOperationException("Given status to delete is null");

            foreach (var connectionToDelete in directions.Select(direction => ConnectedStatuses.FirstOrDefault(s => s.ConnectedStatusInFlow.Id == status.Id && s.Direction == direction)))
            {
                if (connectionToDelete == null)
                    continue; //already removed

                ConnectedStatuses.Remove(connectionToDelete);
                AddDomainEvent(new ConnectedStatusRemovedDomainEvent(connectionToDelete));
            }
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
