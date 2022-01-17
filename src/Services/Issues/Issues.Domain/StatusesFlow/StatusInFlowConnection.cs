using System;
using System.Collections.Generic;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        public StatusInFlowConnection(StatusInFlow statusInFlow, StatusInFlowDirection direction, StatusInFlow connectedStatus)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedStatus = connectedStatus;
            ConnectedStatusId = connectedStatus.Id;
            Direction = direction;
            ParentStatusInFlow = statusInFlow;
            ParentStatusInFlowId = statusInFlow.Id;
        }

        public StatusInFlowConnection()
        {
                
        }
        public StatusInFlowDirection Direction { get; set; }
        public StatusInFlow ConnectedStatus { get; set; }
        public StatusInFlow ParentStatusInFlow { get; set; }
        public string ParentStatusInFlowId { get; set; }
        public string ConnectedStatusId { get; set; }
    }

    public enum StatusInFlowDirection
    {
        In = 0,
        Out = 1,
    }
}