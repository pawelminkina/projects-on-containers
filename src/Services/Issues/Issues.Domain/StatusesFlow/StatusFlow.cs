using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public class StatusFlow : EntityBase
    {
        public StatusFlow(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }
        public StatusFlow()
        {

        }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public List<StatusInFlow> StatusesInFlow { get; set; }

        public StatusInFlow AddNewStatusInFlow(Status statusToAdd, int indexInFlow)
        {
            var status = new StatusInFlow(statusToAdd, this, indexInFlow);
            StatusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusInFlow(string statusInFlowId)
        {
            var statusToDelete = StatusesInFlow.FirstOrDefault(a => a.Id == statusInFlowId);
            if (statusToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusInFlowId} doesn't exist");
            StatusesInFlow.Remove(statusToDelete);
        }
    }
}
