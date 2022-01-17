using System;
using Architecture.DDD;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain.StatusFlowInGroup
{
    public class StatusFlowInGroupOfIssue : EntityBase, IAggregateRoot
    {
        public StatusFlowInGroupOfIssue(string groupOfIssueId, string statusFlowId)
        {
            Id = Guid.NewGuid().ToString();
            GroupOfIssuesId = groupOfIssueId;
            StatusFlowId = statusFlowId;
        }

        public string GroupOfIssuesId { get; set; }
        public GroupOfIssues GroupOfIssues { get; set; }
        public string StatusFlowId { get; set; }
        public StatusFlow StatusFlow { get; set; }
    }
}
