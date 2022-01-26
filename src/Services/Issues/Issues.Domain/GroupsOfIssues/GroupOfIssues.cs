 using Architecture.DDD;
using Issues.Domain.Issues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
 using Issues.Domain.GroupsOfIssues.DomainEvents;
 using Issues.Domain.StatusesFlow;

namespace Issues.Domain.GroupsOfIssues
{
    public class GroupOfIssues : EntityBase, IDeletableEntity
    {
        internal const int MinShortNameLength = 3;
        internal const int MaxShortNameLength = 5;
        internal const int TimeInDaysKeptInThrash = 60;
        
        public GroupOfIssues(string name, string shortName, TypeOfGroupOfIssues typeOfGroupOfIssues) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            ShortName = shortName;
            TypeOfGroupId = typeOfGroupOfIssues.Id;
            TypeOfGroup = typeOfGroupOfIssues;
        }

        public GroupOfIssues()
        {
            _issues = new List<Issue>();
        }

        internal static GroupOfIssues CreateWholeObject(string id, string name, string shortName, string typeOfGroupId, string connectedStatusFlowId, bool isDeleted, DateTimeOffset? timeOfDelete)
        {
            return null;
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TypeOfGroupId { get; set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; set; }
        public string ConnectedStatusFlowId { get; set; }
        public StatusFlow ConnectedStatusFlow { get; set; }

        protected readonly List<Issue> _issues;
        public IReadOnlyCollection<Issue> Issues => _issues;

        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {

            if (IsDeleted)
                throw new InvalidOperationException($"Add issue operation failed because group of issues with Id: {Id} is deleted");

            var issue = new Issue(name, creatingUserId, this, DateTimeOffset.UtcNow, textContent);
            _issues.Add(issue);
            return issue;
        }

        public void DeleteIssue(string issueId)
        {
            var issueToRemove = _issues.FirstOrDefault(a => a.Id == issueId);
            if (issueToRemove is null)
                throw new InvalidOperationException($"Requested issue to remove with id: {issueId} is not added in group with {Id}");

            if (IsDeleted)
                throw new InvalidOperationException($"Delete operation failed because group of issues with Id: {Id} is deleted");

            if (issueToRemove.IsDeleted)
                throw new InvalidOperationException($"Delete operation failed because issue with Id: {Id} is already deleted");


            issueToRemove.SetIsDeletedToTrue();
        }

        internal void RemoveIssueFromGroup(string issueId)
        {
            var issueToRemove = _issues.FirstOrDefault(a => a.Id == issueId);
            if (issueToRemove is null)
                throw new InvalidOperationException($"Requested issue to remove with id: {issueId} is not added in group with {Id}");

            _issues.Remove(issueToRemove);
        }

        public void Rename(string newName)
        {
            if (IsDeleted)
                throw new InvalidOperationException($"Group of issue with id: {Id} is deleted, that's why modify operation is not possible");

            ChangeStringProperty("Name", newName);

            AddDomainEvent(new GroupOfIssuesNameChangedDomainEvent(this));
        } 

        public void ChangeShortName(string newShortName)
        {
            if (IsDeleted)
                throw new InvalidOperationException($"Cannot change short name of group with id: {Id} which is deleted");

            if (newShortName.Length is > MaxShortNameLength or < MinShortNameLength)
                throw new InvalidOperationException($"Requested new short name: {newShortName} have more cases then {MaxShortNameLength} or has less cases then {MinShortNameLength}");

            ChangeStringProperty("ShortName", newShortName);
            AddDomainEvent(new GroupOfIssuesShortNameChangedDomainEvent(this));
            //TODO shortname changed event domain, and check that any of those already exist in db _groupOfIssuesRepository.AnyOfGroupHasGivenShortNameAsync
        }

        #region Delete

        public bool IsDeleted { get; set; }
        public DateTimeOffset? TimeOfDeleteUtc { get; set; }

        internal void Delete()
        {
            IsDeleted = true;
            TimeOfDeleteUtc = DateTimeOffset.UtcNow;
            AddDomainEvent(new GroupOfIssuesDeletedDomainEvent(this));
        }

        internal void UndoDelete()
        {
            IsDeleted = false;
            TimeOfDeleteUtc = null;
            AddDomainEvent(new GroupOfIssuesUndoDeletedDomainEvent(this));
        }

        public bool IsInThrash() => TimeOfDeleteUtc?.AddDays(TimeInDaysKeptInThrash) > DateTimeOffset.UtcNow;


        #endregion


    }
}
