using System.Collections.Generic;
using System.Net.WebSockets;
using Issues.API.Protos;

namespace Issues.API.Infrastructure.Factories
{
    public static class GrpcStatusInFlowFactory
    {
        public static StatusInFlow Create(string id, string name, IEnumerable<string> connectedStatusesIds, bool isDefault = false)
        {
            var status = new StatusInFlow()
            {
                Id = id,
                Name = name,
                IsDefault = isDefault
            };
            if (connectedStatusesIds is not null)
                status.ConnectedStatusesId.AddRange(connectedStatusesIds);
            return status;
        }
    }
}
