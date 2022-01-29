 using Architecture.DDD;
using Issues.Domain.Issues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
 using Architecture.DDD.Exceptions;
 using Issues.Domain.GroupsOfIssues.DomainEvents;
 using Issues.Domain.StatusesFlow;

namespace Issues.Domain.GroupsOfIssues
{
    public class GroupOfIssues : EntityBase, IDeletableEntity
    {
        internal const int MinShortNameLength = 3;
        internal const int MaxShortNameLength = 5;
        internal const int TimeInDaysKeptInThrash = 60;
        
        internal GroupOfIssues(string name, string shortName, TypeOfGroupOfIssues typeOfGroupOfIssues) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            ShortName = shortName;
            TypeOfGroup = typeOfGroupOfIssues;
        }

        protected GroupOfIssues()
        {
            _issues = new List<Issue>();
        }

        internal static GroupOfIssues CreateWholeObject(string id, string name, string shortName, string typeOfGroupId, string connectedStatusFlowId, bool isDeleted, DateTimeOffset? timeOfDelete)
        {
            return new GroupOfIssues()
            {
                Id = id,
                _connectedStatusFlowId = connectedStatusFlowId,
                _typeOfGroupId = typeOfGroupId,
                IsDeleted = isDeleted,
                Name = name,
                ShortName = shortName,
                TimeOfDeleteUtc = timeOfDelete
            };
        }
        public string Name { get; protected set; }
        public string ShortName { get; protected set; }

        private string _typeOfGroupId;
        public TypeOfGroupOfIssues TypeOfGroup { get; protected set; }
        
        private string _connectedStatusFlowId;
        public StatusFlow ConnectedStatusFlow { get; protected set; }

        protected readonly List<Issue> _issues;
        public IReadOnlyCollection<Issue> Issues => _issues;

        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {

            if (IsDeleted)
                throw new DomainException(ErrorMessages.ModifyOperationFailedBecauseGroupOfIssuesIsDeleted(Id));

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
                throw new DomainException(ErrorMessages.ModifyOperationFailedBecauseGroupOfIssuesIsDeleted(Id));

            if (issueToRemove.IsDeleted)
                throw new InvalidOperationException($"Delete operation failed because issue with Id: {Id} is already deleted");


            issueToRemove.SetIsDeletedToTrue();
        }

        public void Rename(string newName)
        {
            if (IsDeleted)
                throw new DomainException(ErrorMessages.ModifyOperationFailedBecauseGroupOfIssuesIsDeleted(Id));

            ChangeStringProperty("Name", newName);

            AddDomainEvent(new GroupOfIssuesNameChangedDomainEvent(this));
        } 

        public void ChangeShortName(string newShortName)
        {
            if (IsDeleted)
                throw new DomainException(ErrorMessages.ModifyOperationFailedBecauseGroupOfIssuesIsDeleted(Id));

            if (!FitsShortNameSize(newShortName))
                throw new DomainException(ErrorMessages.RequestedShortNameHasNotRequiredSize(newShortName));

            ChangeStringProperty("ShortName", newShortName);
            AddDomainEvent(new GroupOfIssuesShortNameChangedDomainEvent(this));
        }

        public static bool FitsShortNameSize(string shortName) => shortName.Length is >= MinShortNameLength and <= MaxShortNameLength;
        

        #region Delete

        public bool IsDeleted { get; protected set; }
        public DateTimeOffset? TimeOfDeleteUtc { get; protected set; }

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

        public static class ErrorMessages
        {
            public static string SomeGroupAlreadyExistWithName(string name) =>
                $"Group of issues with name: {name} already exist";

            public static string SomeGroupAlreadyExistWithShortName(string shortName) =>
                $"Group of issues with short name: {shortName} already exist";

            public static string ModifyOperationFailedBecauseGroupOfIssuesIsDeleted(string id) =>
                $"Modify operation failed because group of issues with id: {id} is deleted";

            public static string RequestedShortNameHasNotRequiredSize(string shortName) =>
                $"Requested new short name: {shortName} have more cases then {MaxShortNameLength} or has less cases then {MinShortNameLength}";
        }

    }
}
