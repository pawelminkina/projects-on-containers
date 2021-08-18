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
        internal GroupOfIssues(string name, string organizationId, string typeOfGroupId, string statusFlowId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            TypeOfGroupId = typeOfGroupId;
            StatusFlowId = statusFlowId;
        }

        internal GroupOfIssues()
        {

        }
        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string TypeOfGroupId { get; protected set; }
        public string StatusFlowId { get; protected set; }
        public List<Issue> Issues { get; protected set; }
        public StatusFlow Flow { get; protected set; } //I need to get it by statusFlow in EF
        public bool IsArchived { get; private set; }

        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {
            var defaultStatus = GetDefaultStatusInFlow();

            var issue = new Issue(name, defaultStatus.ParentStatus.Id, creatingUserId, this.Id, DateTimeOffset.UtcNow);
            issue.AddContent(textContent);
            Issues.Add(issue);
            return issue;
        }


        public Issue AssignIssueToGroup(Issue existingIssue)
        {
            var issueToAdd = Issues.FirstOrDefault(a => a.Id == existingIssue.Id);
            if (issueToAdd != null)
                throw new InvalidOperationException(
                    $"Requested issue to assign with id: {existingIssue.Id} is already added in group with {Id}");

            var defaultStatus = GetDefaultStatusInFlow();

            existingIssue.ChangeStatus(defaultStatus.ParentStatus.Id);
            existingIssue.ChangeGroupOfIssue(Id);
            Issues.Add(existingIssue);
            return existingIssue;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");

            if (newName == Name)
                throw new InvalidOperationException("Requested new name is the same as current");

            Name = newName;
        }

        public void Archive()
        {
            Issues.ForEach(s=>s.Archive());
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }

        private StatusInFlow GetDefaultStatusInFlow() => Flow.StatusesInFlow.FirstOrDefault(d=>d.IndexInFlow == 0) ?? throw new InvalidOperationException($"Default status don't exist in status flow with id: {Flow.Id}");
    }
}
