using System.Collections.Generic;
using Issues.API.Protos;

namespace Issues.API.Infrastructure.Factories
{
    public static class GrpcStatusFlowFactory
    {
        public static StatusFlow Create(string id, string name, IEnumerable<StatusInFlow> statusesInFlow, bool isDefault = false)
        {
            var statusFlow = new StatusFlow()
            {
                Id = id,
                Name = name,
                IsDefault = isDefault
            };
            if (statusesInFlow is not null)
                statusFlow.Statuses.AddRange(statusesInFlow);
            return statusFlow;
        }
    }
}
