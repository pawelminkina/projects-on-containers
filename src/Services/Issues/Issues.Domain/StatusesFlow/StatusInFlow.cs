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
        public StatusInFlow(Status parentStatus, StatusFlow statusFlow, int indexInFlow) : this()
        {
            Id = Guid.NewGuid().ToString();
            ParentStatus = parentStatus;
            StatusFlow = statusFlow;
            IndexInFlow = indexInFlow;
            IsArchived = false;
        }
        public StatusInFlow()
        {
            ConnectedStatuses = new List<StatusInFlowConnection>();
        }
        public Status ParentStatus { get; set; }
        public string ParentStatusId { get; set; }
        public StatusFlow StatusFlow { get; set; }
        public string StatusFlowId { get; set; }
        public int IndexInFlow { get; set; }
        public bool IsArchived { get; set; }

        public List<StatusInFlowConnection> ConnectedStatuses { get; private set; } //dunno how i will do this xD, functional tests will show

        public void AddConnectedStatus(Status status, StatusInFlowDirection direction)
        {
            if (status == null)
                throw new InvalidOperationException("Given status to add is null");

            if (ConnectedStatuses.Any(s=> s.ConnectedStatus.Id == status.Id && s.Direction == direction))
                throw new InvalidOperationException(
                    $"Status with connectedStatusId: {status.Id} is already added to connected statuses where status in flow has connectedStatusId: {Id} with direction of connection: {direction.ToString()}");

            var connectedStatus = new StatusInFlowConnection(this, direction, status);
            ConnectedStatuses.Add(connectedStatus);
            AddDomainEvent(new ConnectedStatusAddedDomainEvent(connectedStatus));
        }

        public void DeleteConnectedStatus(string connectedStatusId, params StatusInFlowDirection[] directions)
        {
            foreach (var connectionToDelete in directions.Select(direction => ConnectedStatuses.FirstOrDefault(s => s.ConnectedStatus.Id == connectedStatusId && s.Direction == direction)))
            {
                if (connectionToDelete == null)
                    throw new InvalidOperationException($"Requested status in flow to delete with connectedStatusId: {connectedStatusId} in parent {Id} doesn't exist");

                ConnectedStatuses.Remove(connectionToDelete);
                AddDomainEvent(new ConnectedStatusRemovedDomainEvent(connectionToDelete));
            }
        }

        public void Archive()
        {
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
