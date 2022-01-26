using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow.DomainEvents;

namespace Issues.Domain.StatusesFlow
{
    public class StatusFlow : EntityBase, IAggregateRoot
    {
        public StatusFlow(string name, string organizationId, GroupOfIssues connectedGroupOfIssues, IEnumerable<string> statusInFlowNames, string nameOfDefaultStatus) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsDeleted = false;
            IsDefault = false;
            ConnectedGroupOfIssues = connectedGroupOfIssues;
            AddDefaultStatusesToFlow(statusInFlowNames, nameOfDefaultStatus);
        }

        public static StatusFlow CreateDefault(string name, string organizationId)
        {
            var statusFlow = new StatusFlow()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                OrganizationId = organizationId,
                IsDeleted = false
            };
            statusFlow.SetDefaultToTrue();
            return statusFlow;
        }

        internal static StatusFlow CreateWholeObject(string id, string name, string organizationId, string connectedGroupOfIssuesId, bool isDefault, bool isDeleted)
        {
            return null;
        }

        protected StatusFlow()
        {
            _statusesInFlow = new List<StatusInFlow>();
        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsDefault { get; protected set; }
        
        private string _connectedGroupOfIssuesId;
        public GroupOfIssues ConnectedGroupOfIssues { get; protected set; }

        protected List<StatusInFlow> _statusesInFlow;
        public IReadOnlyCollection<StatusInFlow> StatusesInFlow => _statusesInFlow;

        public bool IsDeleted { get; protected set; }

        public StatusInFlow AddNewStatusToFlow(string statusName)
        {
            var statusCurrentlyExistInFlow = StatusesInFlow.Any(s => string.Equals(s.Name, statusName, StringComparison.CurrentCultureIgnoreCase));
            if (statusCurrentlyExistInFlow)
                throw new InvalidOperationException(
                    $"Requested status to add with name: {statusName} currently exist in flow with id: {Id}");

            var status = new StatusInFlow(this, statusName, false);
            _statusesInFlow.Add(status);
            return status;
        }

        public void DeleteStatusFromFlow(string statusInFlowId)
        {
            var statusInFlowToDelete = StatusesInFlow.FirstOrDefault(a => string.Equals(a.Id, statusInFlowId, StringComparison.CurrentCultureIgnoreCase));
            if (statusInFlowToDelete == null)
                throw new InvalidOperationException(
                    $"Requested status to delete with id: {statusInFlowId} doesn't exist in flow with id: {Id}");

            if (statusInFlowToDelete.IsDefault)
                throw new InvalidOperationException($"Could not delete default status with id: {statusInFlowId}");

            AddDomainEvent(new StatusInFlowDeletedDomainEvent(statusInFlowToDelete));
            _statusesInFlow.Remove(statusInFlowToDelete);
        }

        public void ChangeDefaultStatusInFlow(string newDefaultName)
        {
            var newDefault = _statusesInFlow.FirstOrDefault(s => s.Name == newDefaultName);
            if (newDefault == null)
                throw new InvalidOperationException($"Requested status to set to default with name: {newDefaultName} don't exist");

            if (newDefault.IsDefault)
                throw new InvalidOperationException("Given status is already default");

            var currentDefault = _statusesInFlow.FirstOrDefault(s => s.IsDefault);

            if (currentDefault == null)
                throw new Exception("There is not default status in flow"); //Should not happen

            currentDefault.SetDefaultToFalse();
            newDefault.SetDefaultToTrue();
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

        private void SetDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new DefaultPropertyInStatusFlowChangedToTrueDomainEvent(this));
;       }

        public void AddDefaultStatusesToFlow(IEnumerable<string> statusNames, string nameOfDefault)
        {
            //TODO have unique values extension
            if (statusNames.Count() == statusNames.Distinct().Count())
                _statusesInFlow.AddRange(statusNames.Select(s => new StatusInFlow(this, s, nameOfDefault == s)));

            throw new InvalidOperationException("Given status name list to create status flow is not unique");
        }

        public static string GetNameWithGroupOfIssues(string groupOfIssuesName) => new StringBuilder("Status flow for: ").Append(groupOfIssuesName).ToString();
    }
}
