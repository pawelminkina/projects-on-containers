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
        public StatusFlow(string name, string organizationId) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsDeleted = false;
            IsDefault = false;
        }
        public StatusFlow()
        {
            _statusesInFlow = new List<StatusInFlow>();
        }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public bool IsDefault { get; set; }

        protected readonly List<StatusInFlow> _statusesInFlow;
        public IReadOnlyCollection<StatusInFlow> StatusesInFlow => _statusesInFlow;

        public bool IsDeleted { get; set; }

        public StatusInFlow AddNewStatusToFlow(string statusName)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => string.Equals(s.Name, statusName, StringComparison.CurrentCultureIgnoreCase));
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with name: {statusName} currently exist in flow with id: {Id}");

            var status = new StatusInFlow(this, statusName);
            _statusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusName)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => string.Equals(a.Name, statusName, StringComparison.CurrentCultureIgnoreCase));
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with name: {statusName} doesn't exist in flow with id: {Id}");

            AddDomainEvent(new StatusInFlowDeletedDomainEvent(statusInFlowToDelete));
            _statusesInFlow.Remove(statusInFlowToDelete);
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Delete()
        {
            if (IsDefault)
                throw new InvalidOperationException("Default status flow could not be deleted");

            IsDeleted = true;
        }

        public void UnDelete()
        {
            IsDeleted = false;
        }
    }
}
