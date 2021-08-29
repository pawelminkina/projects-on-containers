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
            //TODO creating status flow should emit some domain event which will be handled with assigning default statuses to this flow
            //TODO so i will need default statuses for all organizations
        }
        protected StatusFlow()
        {

        }
        public string Name { get; private set; }
        public string OrganizationId { get; private set; }
        public List<StatusInFlow> StatusesInFlow { get; set; }
        public bool IsArchived { get; private set; }

        public StatusInFlow AddNewStatusToFlow(Status statusToAdd)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => s.ParentStatus.Id == statusToAdd.Id);
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with id: {statusToAdd.Id} currently exist in flow with id: {Id}");

            var highestIndex = StatusesInFlow.Max(s => s.IndexInFlow);
            var status = new StatusInFlow(statusToAdd, this, highestIndex + 1);
            StatusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusId)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => a.ParentStatus.Id == statusId);
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusId} doesn't exist in flow with id: {Id}");
            
            //TODO status from flow delete domain event which will remove it from db and all connections
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
