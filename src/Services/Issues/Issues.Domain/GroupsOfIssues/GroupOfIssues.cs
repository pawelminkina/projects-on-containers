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
        public GroupOfIssues(string name, string organizationId, string typeOfGroupId, string statusFlowId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            TypeOfGroupId = typeOfGroupId;
            StatusFlowId = statusFlowId;
        }

        public GroupOfIssues()
        {

        }

        public string Name { get; }
        public string OrganizationId { get; }
        public string TypeOfGroupId { get; }
        public string StatusFlowId { get; }
        public List<Issue> Issues { get; set; }
        public StatusFlow Flow { get; set; } //I need to get it by statusFlow in EF
        public Issue AddIssue(string name, string creatingUserId, string textContent)
        {
            var defaultStatus = Flow.StatusesInFlow.FirstOrDefault(s => s.IsDefault);
            if (defaultStatus == null)
                throw new InvalidOperationException($"Default status don't exist in status flow with id: {Flow.Id}");

            var issue = new Issue(name, defaultStatus.Id, creatingUserId, Id, DateTimeOffset.UtcNow);
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
    }
}
