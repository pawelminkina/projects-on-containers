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
    public class GroupOfIssues : EntityBase
    {
        public GroupOfIssues(string name, TypeOfGroupOfIssues typeOfGroupOfIssues) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            TypeOfGroupId = typeOfGroupOfIssues.Id;
            TypeOfGroup = typeOfGroupOfIssues;
            IsArchived = false;
        }

        //TODO maybe group of issues could have not unique name, but they should have short name used for url and identification which need to be unique 
        //if so check application layer and delete all checking that name is the same
        protected GroupOfIssues()
        {
            _issues = new List<Issue>();
        }
        public string Name { get; private set; }
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
