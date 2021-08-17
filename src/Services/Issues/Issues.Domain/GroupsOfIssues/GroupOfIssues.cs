using Architecture.DDD;
using Issues.Domain.Issues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        private GroupOfIssues()
        {

        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string TypeOfGroupId { get; protected set; }
        public string StatusFlowId { get; protected set; }
        public List<Issue> Issues { get; protected set; }
        public virtual StatusFlow Flow { get; protected set; } //I need to get it by statusFlow in EF
        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {
            var defaultStatus = Flow.StatusesInFlow.FirstOrDefault(s => s.IndexInFlow == 0);
            if (defaultStatus == null)
                throw new InvalidOperationException($"Default status don't exist in status flow with id: {Flow.Id}");

            var issue = new Issue(name, defaultStatus.ParentStatus.Id, creatingUserId, this.Id, DateTimeOffset.UtcNow);
            issue.AddContent(textContent);
            Issues.Add(issue);
            return issue;
        }

        public void DeleteIssue(string id)
        {
            var issue = Issues.FirstOrDefault(a => a.Id == id);
            if (issue == null)
                throw new InvalidOperationException($"Issue with id {id} don't exist in group of issues with id: {Id}");
            
            issue.DeleteContent();
            Issues.Remove(issue);
        }

        public Issue AddIssue(Issue existingIssue)
        {
            var issueToAdd = Issues.FirstOrDefault(a => a.Id == existingIssue.Id);
            if (issueToAdd != null)
                throw new InvalidOperationException(
                    $"Requested issue to add with id: {existingIssue.Id} is already added in group with {Id}");

            var defaultStatus = Flow.StatusesInFlow.FirstOrDefault(s => s.IndexInFlow == 0);
            if (defaultStatus == null)
                throw new InvalidOperationException($"Default status don't exist in status flow with id: {Flow.Id}");

            existingIssue.ChangeStatus(defaultStatus.ParentStatus.Id);
            existingIssue.ChangeGroupOfIssue(Id);
            Issues.Add(existingIssue);
            return existingIssue;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");
            Name = newName;
        }
    }
}
