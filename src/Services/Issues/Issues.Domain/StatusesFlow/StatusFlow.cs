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
        internal StatusFlow(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }
        private StatusFlow()
        {

        }
        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public List<StatusInFlow> StatusesInFlow { get; protected set; } 

        public StatusInFlow AddNewStatusToFlow(Status statusToAdd, int indexInFlow)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => s.ParentStatus.Id == statusToAdd.Id);
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with id: {statusToAdd.Id} currently exist in flow with id: {Id}");

            var status = new StatusInFlow(statusToAdd, this, indexInFlow);
            StatusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusId)
        {
            var statusToDelete = StatusesInFlow.FirstOrDefault(a => a.ParentStatus.Id == statusId);
            if (statusToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusId} doesn't exist in flow with id: {Id}");
            StatusesInFlow.Remove(statusToDelete);
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");

            Name = newName;
        }
    }
}
