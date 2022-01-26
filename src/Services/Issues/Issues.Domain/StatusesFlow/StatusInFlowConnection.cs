using System;
using System.Collections.Generic;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        public StatusInFlowConnection(StatusInFlow statusInFlow, StatusInFlowDirection direction, StatusInFlow connectedStatusInFlow)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedStatusInFlow = connectedStatusInFlow;
            ConnectedStatusInFlowId = connectedStatusInFlow.Id;
            Direction = direction;
            ParentStatusInFlow = statusInFlow;
            ParentStatusInFlowId = statusInFlow.Id;
        }

        public StatusInFlowConnection()
        {
                
        }

        internal static StatusInFlowConnection CreateWholeObject(string id, string parentId, string connectedId, StatusInFlowDirection direction)
        {
            return null;
        }
        public StatusInFlowDirection Direction { get; set; }
        public StatusInFlow ConnectedStatusInFlow { get; set; }
        public StatusInFlow ParentStatusInFlow { get; set; }
        public string ParentStatusInFlowId { get; set; }
        public string ConnectedStatusInFlowId { get; set; }
    }

    public enum StatusInFlowDirection
    {
        In = 0,
        Out = 1,
    }
}