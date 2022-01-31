using System.Collections.Generic;
using System.Net.WebSockets;
using Issues.API.Protos;

namespace Issues.API.Infrastructure.Factories
{
    public static class GrpcStatusInFlowFactory
    {
        public static StatusInFlow Create(string id, string name, IEnumerable<ConnectedStatuses> connectedStatuses, bool isDefault = false)
        {
            var status = new StatusInFlow()
            {
                Id = id,
                Name = name,
                IsDefault = isDefault
            };
            if (connectedStatuses is not null)
                status.ConnectedStatuses.AddRange(connectedStatuses);
            return status;
        }
    }
}
