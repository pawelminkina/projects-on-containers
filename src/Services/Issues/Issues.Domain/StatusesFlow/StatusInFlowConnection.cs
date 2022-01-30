using System;
using System.Collections.Generic;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        internal StatusInFlowConnection(StatusInFlow statusInFlow, StatusInFlow connectedStatusInFlow)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedStatusInFlow = connectedStatusInFlow;
            ParentStatusInFlow = statusInFlow;
        }

        protected StatusInFlowConnection()
        {
                
        }

        internal static StatusInFlowConnection CreateWholeObject(string id, string parentId, string connectedId)
        {
            return new StatusInFlowConnection()
            {
                Id = id,
                _parentStatusInFlowId = parentId,
                _connectedStatusInFlowId = connectedId,
            };
        }
        public StatusInFlow ConnectedStatusInFlow { get; set; }
        public StatusInFlow ParentStatusInFlow { get; set; }
        private string _parentStatusInFlowId;
        private string _connectedStatusInFlowId;
    }
}