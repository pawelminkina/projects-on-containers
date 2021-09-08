 using Architecture.DDD;
using Issues.Domain.Issues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain.GroupsOfIssues
{
    //TODO archive and delete policy:
    //type of group of issue could be archived if not default, so it will need default property (or not, maybe smth which will hold all default values? = nameoftable, orgid, id), and if archived all groups will be assigne to default/ chosen
    //group of issues: could be archived, and all issues will be archived too, or all issues will be moved to another group, and group will be deleted permanently
    //issue: could be deleted by setting isDeletedProperty, so it will never disappear from db, also could be set archived but only from groupOfIssues
    public class GroupOfIssues : EntityBase
    {
        internal const int MinShortNameLength = 3;
        internal const int MaxShortNameLength = 5;
        
        public GroupOfIssues(string name, string shortName, TypeOfGroupOfIssues typeOfGroupOfIssues) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            ShortName = shortName;
            TypeOfGroupId = typeOfGroupOfIssues.Id;
            TypeOfGroup = typeOfGroupOfIssues;
            IsArchived = false;
        }

        protected GroupOfIssues()
        {
            _issues = new List<Issue>();
        }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string TypeOfGroupId { get; private set; }
        public StatusFlow Flow { get; private set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; private set; } 
        public bool IsArchived { get; private set; }

        protected readonly List<Issue> _issues;
        public IReadOnlyCollection<Issue> Issues => _issues;

        public Issue AddIssue(string name, string creatingUserId, string textContent, string typeOfIssueId)
        {
            var defaultStatus = GetDefaultStatusInFlow();

            var issue = new Issue(name, defaultStatus.ParentStatus.Id, creatingUserId, this, DateTimeOffset.UtcNow, typeOfIssueId);
            issue.AddContent(textContent);
            _issues.Add(issue);
            return issue;
        }


        public Issue AssignIssueToGroup(Issue existingIssue, string newStatusId)
        {

            if (string.IsNullOrWhiteSpace(newStatusId))
                throw new InvalidOperationException("Given new statusId is empty string");

            var issueToAdd = _issues.FirstOrDefault(a => a.Id == existingIssue.Id);
            if (issueToAdd != null)
                throw new InvalidOperationException(
                    $"Requested issue to assign with id: {existingIssue.Id} is already added in group with {Id}");

            existingIssue.ChangeStatus(newStatusId);
            existingIssue.ChangeGroupOfIssue(this);
            _issues.Add(existingIssue);
            return existingIssue;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeShortName(string newShortName)
        {
            if (newShortName.Length is > MaxShortNameLength or < MinShortNameLength)
                throw new InvalidOperationException($"Requested new short name: {newShortName} have more cases then {MaxShortNameLength} or has less cases then {MinShortNameLength}");

            ChangeStringProperty("ShortName", newShortName);
        }

        public void Archive()
        {
            _issues.ForEach(s=>s.Archive());
            IsArchived = true;
        }

        public void UnArchive()
        {
            _issues.ForEach(s => s.UnArchive());
            IsArchived = false;
        }

        private StatusInFlow GetDefaultStatusInFlow() => Flow.StatusesInFlow.FirstOrDefault(d=>d.IndexInFlow == 0) ?? throw new InvalidOperationException($"Default status don't exist in status flow with id: {Flow.Id}");
    }
}
