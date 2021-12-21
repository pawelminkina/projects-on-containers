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
            IsDefault = false;
        }
        public StatusFlow()
        {

        }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public bool IsDefault { get; set; }

        protected readonly List<StatusInFlow> _statusesInFlow;
        public IReadOnlyCollection<StatusInFlow> StatusesInFlow => _statusesInFlow;

        public bool IsArchived { get; set; }

        public StatusInFlow AddNewStatusToFlow(Status statusToAdd)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => s.ParentStatus.Id == statusToAdd.Id);
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with id: {statusToAdd.Id} currently exist in flow with id: {Id}");

            var status = new StatusInFlow(statusToAdd, this, StatusesInFlow.Any() ? StatusesInFlow.Max(s => s.IndexInFlow): 0);
            _statusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusId)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => a.ParentStatus.Id == statusId);
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusId} doesn't exist in flow with id: {Id}");

            AddDomainEvent(new StatusInFlowDeletedDomainEvent(statusInFlowToDelete));
            _statusesInFlow.Remove(statusInFlowToDelete);
        }

        public void SetIsDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new StatusFlowSettedToDefaultDomainEvent(this));
        }

        public void SetIsDefaultToFalse()
        {
            IsDefault = true;
#warning there exist a possibility, that it will don't work because context was not saved, and i will have to delete validation, or make it from application level.
            AddDomainEvent(new StatusFlowUnsettedFromDefaultDomainEvent(this)); 
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Archive()
        {
            if (IsDefault)
                throw new InvalidOperationException("Default status flow could not be archived");


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
