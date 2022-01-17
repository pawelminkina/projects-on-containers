using System.Collections.Generic;
using System.Net.WebSockets;
using Issues.API.Protos;

namespace Issues.API.Infrastructure.Factories
{
    public static class GrpcStatusInFlowFactory
    {
        public static StatusInFlow Create(string parentStatusId, int indexInFlow, IEnumerable<ConnectedStatuses> connectedStatuses)
        {
            var status = new StatusInFlow()
            {
                ParentStatusId = parentStatusId,
                IndexInFLow = indexInFlow
            };
            if (connectedStatuses is not null)
                status.ConnectedStatuses.AddRange(connectedStatuses);
            return status;
        }
    }
}
