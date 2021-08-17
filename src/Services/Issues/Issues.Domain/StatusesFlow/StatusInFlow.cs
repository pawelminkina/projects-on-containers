using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlow : EntityBase
    {
        internal StatusInFlow(Status parentStatus, StatusFlow statusFlow, int indexInFlow)
        {
            Id = Guid.NewGuid().ToString();
            ParentStatus = parentStatus;
            StatusFlow = statusFlow;
            IndexInFlow = indexInFlow;
            ConnectedStatuses = new List<Status>();
        }
        private StatusInFlow()
        {
            ConnectedStatuses = new List<Status>();
        }
        public Status ParentStatus { get; protected set; }
        //one to many
        public StatusFlow StatusFlow { get; protected set; }
        public int IndexInFlow { get; protected set; }

        public readonly List<Status> ConnectedStatuses;
        public void AddConnectedStatus(Status status)
        {
            if (status == null)
                throw new InvalidOperationException("Given status to add is null");
            if (ConnectedStatuses.Contains(status))
                throw new InvalidOperationException(
                    $"Status with id: {status?.Id} is already added to connected statuses where parent status has id: {Id}");

            ConnectedStatuses.Add(status);
        }

        public void DeleteConnectionStatus(string id)
        {

            var connectionToDelete = ConnectedStatuses.FirstOrDefault(s => s.Id == id);
            if (connectionToDelete == null)
                throw new InvalidOperationException($"Requested status in flow to delete with id: {id} in parent {Id} doesn't exist");

            ConnectedStatuses.Remove(connectionToDelete);
        }
    }
}
