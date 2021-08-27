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
        public GroupOfIssues(string name, TypeOfGroupOfIssues typeOfGroupOfIssues, string statusFlowId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = typeOfGroupOfIssues.OrganizationId;
            TypeOfGroupId = typeOfGroupOfIssues.Id;
            TypeOfGroup = typeOfGroupOfIssues;
            StatusFlowId = statusFlowId;
            IsArchived = false;
        }

        public GroupOfIssues()
        {

        }
        public virtual string Name { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual string TypeOfGroupId { get; set; }
        public virtual string StatusFlowId { get; set; }
        public virtual List<Issue> Issues { get; set; }
        public virtual StatusFlow Flow { get; set; }
        public virtual TypeOfGroupOfIssues TypeOfGroup { get; set; } 
        public virtual bool IsArchived { get; set; }

        public Issue AddIssue(string name, string creatingUserId, string textContent, string typeOfIssueId)
        {
            var defaultStatus = GetDefaultStatusInFlow();

            var issue = new Issue(name, defaultStatus.ParentStatus.Id, creatingUserId, this, DateTimeOffset.UtcNow, typeOfIssueId, OrganizationId);
            issue.AddContent(textContent);
            Issues.Add(issue);
            return issue;
        }


        public Issue AssignIssueToGroup(Issue existingIssue, string newStatusId)
        {

            if (string.IsNullOrWhiteSpace(newStatusId))
                throw new InvalidOperationException("Given new statusId is empty string");

            var issueToAdd = Issues.FirstOrDefault(a => a.Id == existingIssue.Id);
            if (issueToAdd != null)
                throw new InvalidOperationException(
                    $"Requested issue to assign with id: {existingIssue.Id} is already added in group with {Id}");

            existingIssue.ChangeStatus(newStatusId);
            existingIssue.ChangeGroupOfIssue(this);
            Issues.Add(existingIssue);
            return existingIssue;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeStatusFlow(string newStatusFlowId) => ChangeStringProperty("StatusFlowId", newStatusFlowId);

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
