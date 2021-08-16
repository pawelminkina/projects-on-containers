using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlowConnection : EntityBase
    {

        public StatusInFlowConnection(string parentStatusInFlowId, string childStatusInFlowId)
        {
            ParentStatusInFlowId = parentStatusInFlowId;
            ChildStatusInFlowId = childStatusInFlowId;
        }

        public StatusInFlowConnection()
        {

        }
        public string ParentStatusInFlowId { get; }
        public string ChildStatusInFlowId { get; }
    }
}
