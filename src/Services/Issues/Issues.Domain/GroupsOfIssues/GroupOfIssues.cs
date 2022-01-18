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
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TypeOfGroupId { get; set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; set; } 

        protected readonly List<Issue> _issues;
        public IReadOnlyCollection<Issue> Issues => _issues;

        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {
            var issue = new Issue(name, creatingUserId, this, DateTimeOffset.UtcNow, textContent);
            _issues.Add(issue);
            return issue;
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
            ChangeStringProperty("Name", newName);
            //TODO group of issue name changed changes name of status flow, and checking that name is unique in db

            AddDomainEvent(new GroupOfIssuesNameChangedDomainEvent(this));
        } 

        public void ChangeShortName(string newShortName)
        {
            if (newShortName.Length is > MaxShortNameLength or < MinShortNameLength)
                throw new InvalidOperationException($"Requested new short name: {newShortName} have more cases then {MaxShortNameLength} or has less cases then {MinShortNameLength}");

            ChangeStringProperty("ShortName", newShortName);
            AddDomainEvent(new GroupOfIssuesShortNameChangedDomainEvent(this));
            //TODO shortname changed event domain, and check that any of those already exist in db
        }

        #region Delete

        public bool IsDeleted { get; set; }
        public DateTimeOffset? TimeOfDeleteUtc { get; set; }

        internal void Delete()
        {
            IsDeleted = true;
            TimeOfDeleteUtc = DateTimeOffset.UtcNow;
            AddDomainEvent(new GroupOfIssuesDeletedDomainEvent(this));
            //TODO archive status flow
        }

        internal void UndoDelete()
        {
            IsDeleted = false;
            TimeOfDeleteUtc = null;
            AddDomainEvent(new GroupOfIssuesUndoDeletedDomainEvent(this));
            //TODO undo archive status flow
        }

        public bool IsInThrash() => TimeOfDeleteUtc?.AddDays(TimeInDaysKeptInThrash) > DateTimeOffset.UtcNow;


        #endregion


    }
}
