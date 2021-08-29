using System;
using Architecture.DDD;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain.TypesOfIssues
{
    public class TypeOfIssueInTypeOfGroup : EntityBase
    {
        public TypeOfIssueInTypeOfGroup(TypeOfIssue parent, string statusFlowId, string typeOfGroupOfIssuesId) : this()
        {
            Id = Guid.NewGuid().ToString();
            Parent = parent;
            StatusFlowId = statusFlowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            IsArchived = false;
        }

        protected TypeOfIssueInTypeOfGroup()
        {
            
        }
        public TypeOfIssue Parent { get; private set; }
        public StatusFlow Flow { get; private set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; private set; }
        public string StatusFlowId { get; private set; }
        public string TypeOfGroupOfIssuesId { get; private set; }
        public bool IsArchived { get; private set; }

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