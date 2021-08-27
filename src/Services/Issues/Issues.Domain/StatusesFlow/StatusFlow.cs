using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;

namespace Issues.Domain.StatusesFlow
{
    public class StatusFlow : EntityBase, IAggregateRoot
    {
        public StatusFlow(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            StatusesInFlow = new List<StatusInFlow>();
            IsArchived = false;
        }
        public StatusFlow()
        {

        }
        public virtual string Name { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual List<StatusInFlow> StatusesInFlow { get; set; }
        public virtual bool IsArchived { get; set; }

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

        public void DeleteStatusFromFlow(string statusId, IStatusInFlowDeletePolicy policy)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => a.ParentStatus.Id == statusId);
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusId} doesn't exist in flow with id: {Id}");
            
            policy.Delete(statusInFlowToDelete.Id);
            StatusesInFlow.Remove(statusInFlowToDelete);
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Archive()
        {
            StatusesInFlow.ForEach(s=>s.Archive());
            IsArchived = true;
        }

        public void UnArchive()
        {
            StatusesInFlow.ForEach(s => s.UnArchive());
            IsArchived = false;
        }
    }
}
