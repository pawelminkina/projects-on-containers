using System;
using System.Collections.Generic;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        internal StatusInFlowConnection(StatusInFlow statusInFlow, StatusInFlowDirection direction, StatusInFlow connectedStatusInFlow)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedStatusInFlow = connectedStatusInFlow;
            Direction = direction;
            ParentStatusInFlow = statusInFlow;
        }

        protected StatusInFlowConnection()
        {
                
        }

        internal static StatusInFlowConnection CreateWholeObject(string id, string parentId, string connectedId, StatusInFlowDirection direction)
        {
            return null;
        }
        public StatusInFlowDirection Direction { get; set; }
        public StatusInFlow ConnectedStatusInFlow { get; set; }
        public StatusInFlow ParentStatusInFlow { get; set; }
        private string _parentStatusInFlowId;
        private string _connectedStatusInFlowId;
    }

    public enum StatusInFlowDirection
    {
        In = 0,
        Out = 1,
    }
}