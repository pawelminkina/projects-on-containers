using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public class AvailableStatusUpdate : EntityBase
    {
        public AvailableStatusUpdate(string availableStatusId, StatusInFlow statusInFlow)
        {
            Id = Guid.NewGuid().ToString();
            AvailableStatusId = availableStatusId;
            StatusInFlow = statusInFlow;
        }

        public AvailableStatusUpdate()
        {

        }

        public string AvailableStatusId { get; }
#warning foregin key to statusinflow
        public StatusInFlow StatusInFlow { get; }
    }
}
