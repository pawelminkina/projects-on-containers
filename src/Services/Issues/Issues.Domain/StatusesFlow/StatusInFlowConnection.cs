using System;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {
        public StatusInFlowConnection(Status connectedWithParent, StatusInFlow parent)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedWithParent = connectedWithParent;
            ParentStatus = parent;

            ParentStatusId = ParentStatus.Id;
            ConnectedWithParentId = ConnectedWithParent.Id;
        }

        public StatusInFlowConnection()
        {
                
        }
        public virtual string ConnectedWithParentId { get; set; }
        public virtual Status ConnectedWithParent { get; set; }
        public virtual string ParentStatusId { get; set; }
        public virtual StatusInFlow ParentStatus { get; set; }
    }
}