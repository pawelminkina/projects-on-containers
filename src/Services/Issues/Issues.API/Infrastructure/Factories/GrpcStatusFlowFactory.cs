using System.Collections.Generic;
using Issues.API.Protos;

namespace Issues.API.Infrastructure.Factories
{
    public static class GrpcStatusFlowFactory
    {
        public static StatusFlow Create(string id, string name, IEnumerable<StatusInFlow> statusesInFlow)
        {
            var statusFlow = new StatusFlow()
            {
                Id = id,
                Name = name
            };
            statusFlow.Statuses.AddRange(statusesInFlow);
            return statusFlow;
        }
    }
}
