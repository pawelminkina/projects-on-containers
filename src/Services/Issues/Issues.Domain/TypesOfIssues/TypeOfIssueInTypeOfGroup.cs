using System;
using Architecture.DDD;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain.TypesOfIssues
{
    public class TypeOfIssueInTypeOfGroup : EntityBase
    {
        public TypeOfIssueInTypeOfGroup(TypeOfIssue parent, string statusFlowId, string typeOfGroupOfIssuesId)
        {
            Id = Guid.NewGuid().ToString();
            Parent = parent;
            StatusFlowId = statusFlowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            IsArchived = false;
        }
        public virtual TypeOfIssue Parent { get; set; }
        public virtual StatusFlow Flow { get; set; }
        public virtual TypeOfGroupOfIssues TypeOfGroup { get; set; }
        public virtual string StatusFlowId { get; set; }
        public virtual string TypeOfGroupOfIssuesId { get; set; }
        public virtual bool IsArchived { get; set; }

        public void ChangeStatusFlow(string newStatusFlowId) => ChangeStringProperty("StatusFlowId", newStatusFlowId);
        public void Archive()
        {
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }

    }
}