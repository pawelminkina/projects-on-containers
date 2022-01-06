using System;
using System.Collections.Generic;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        public StatusInFlowConnection(StatusInFlow statusInFlow, Status parent, StatusInFlowDirection direction, Status connectedStatus)
        {
            Id = Guid.NewGuid().ToString();
            ParentStatus = parent;
            ParentStatusId = parent.Id;
            ConnectedStatus = connectedStatus;
            ConnectedStatusId = connectedStatus.Id;
            Direction = direction;
            ParentStatusInFlow = statusInFlow;
            ParentStatusInFlowId = statusInFlow.Id;
        }

        protected StatusInFlowConnection()
        {
                
        }
        public StatusInFlowDirection Direction { get; set; }
        public Status ParentStatus { get; set; }
        public Status ConnectedStatus { get; set; }
        public StatusInFlow ParentStatusInFlow { get; set; }
        public string ParentStatusInFlowId { get; set; }
        public string ParentStatusId { get; private set; }
        public string ConnectedStatusId { get; private set; }
    }

    public enum StatusInFlowDirection
    {
        In = 0,
        Out = 1,
    }
}