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
        public StatusInFlow(Status parentStatus, StatusFlow statusFlow, int indexInFlow)
        {
            Id = Guid.NewGuid().ToString();
            ParentStatus = parentStatus;
            StatusFlow = statusFlow;
            IndexInFlow = indexInFlow;
            ConnectedStatuses = new List<StatusInFlow>();
        }
        public StatusInFlow()
        {
            ConnectedStatuses = new List<StatusInFlow>();
        }
        public Status ParentStatus { get; }
        //one to many
        public StatusFlow StatusFlow { get; }
        public int IndexInFlow { get; }
        //would be nice if it will exist in db, how dunno
        //got idea: who if it will be populated by using StatusInFlowConnection from DB
        public readonly List<StatusInFlow> ConnectedStatuses;
        private readonly List<StatusInFlowConnection> _connections;
        public void AddConnectedStatus(StatusInFlow status)
        {
            var connection = new StatusInFlowConnection(Id, status.Id);
            _connections.Add(connection);
            ConnectedStatuses.Add(status);
        }

        public void DeleteConnectionStatus(string id)
        {
            var connection =
                _connections.FirstOrDefault(s => s.ParentStatusInFlowId == Id && s.ChildStatusInFlowId == id);
            if (connection == null)
                throw new InvalidOperationException($"Requested status in flow to delete with id: {id} in parent {Id} doesn't exist");

            _connections.Remove(connection);

            var connectionToDelete = ConnectedStatuses.FirstOrDefault(s => s.Id == id);
            if (connectionToDelete == null)
                throw new InvalidOperationException($"Requested status in flow to delete with id: {id} doesn't exist");
            
            ConnectedStatuses.Remove(connectionToDelete);
        }
    }
}
