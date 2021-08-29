using System;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        public StatusInFlowConnection(Status connectedWithParent, StatusInFlow parent) : this()
        {
            Id = Guid.NewGuid().ToString();
            ConnectedWithParent = connectedWithParent;
            ParentStatus = parent;

            ParentStatusId = ParentStatus.Id;
            ConnectedWithParentId = ConnectedWithParent.Id;
        }

        protected StatusInFlowConnection()
        {
                
        }
        public string ConnectedWithParentId { get; private set; }
        public Status ConnectedWithParent { get; private set; }
        public string ParentStatusId { get; private set; }
        public StatusInFlow ParentStatus { get; private set; }
    }
}