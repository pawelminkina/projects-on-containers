using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.StatusesFlow.DomainEvents;

namespace Issues.Domain.StatusesFlow
{
    public class StatusFlow : EntityBase, IAggregateRoot
    {
        public StatusFlow(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            _statusesInFlow = new List<StatusInFlow>();
            IsArchived = false;
        }
        protected StatusFlow()
        {

        }
        public string Name { get; private set; }
        public string OrganizationId { get; private set; }

        protected readonly List<StatusInFlow> _statusesInFlow;
        public IReadOnlyCollection<StatusInFlow> StatusesInFlow => _statusesInFlow;

        public bool IsArchived { get; private set; }

        public StatusInFlow AddNewStatusToFlow(Status statusToAdd)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => s.ParentStatus.Id == statusToAdd.Id);
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with id: {statusToAdd.Id} currently exist in flow with id: {Id}");

            var highestIndex = StatusesInFlow.Max(s => s.IndexInFlow);
            var status = new StatusInFlow(statusToAdd, this, highestIndex + 1);
            _statusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusId)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => a.ParentStatus.Id == statusId);
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusId} doesn't exist in flow with id: {Id}");

            //TODO status from flow delete domain event which will remove it from db and all connections
            _statusesInFlow.Remove(statusInFlowToDelete);
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Archive()
        {
            _statusesInFlow.ForEach(s=>s.Archive());
            IsArchived = true;
            AddDomainEvent(new StatusFlowArchivedDomainEvent(this));
        }

        public void UnArchive()
        {
            _statusesInFlow.ForEach(s => s.UnArchive());
            IsArchived = false;
        }
    }
}
