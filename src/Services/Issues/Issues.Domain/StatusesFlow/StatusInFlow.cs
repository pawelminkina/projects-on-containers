using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public class StatusInFlow : EntityBase
    {
        public StatusInFlow(string name, StatusFlow statusFlow)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusFlow = statusFlow;
        }
        public StatusInFlow()
        {

        }
        public string Name { get; }
        public StatusFlow StatusFlow { get; }
#warning one to many
        public IEnumerable<AvailableStatusUpdate> AvailableStatusUpdates { get; }
    }
}
