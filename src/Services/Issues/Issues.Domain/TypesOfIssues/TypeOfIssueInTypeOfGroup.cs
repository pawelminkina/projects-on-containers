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
            ParentId = parent.Id;
            StatusFlowId = statusFlowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            IsArchived = false;
        }

        public TypeOfIssueInTypeOfGroup()
        {
            
        }
        public TypeOfIssue Parent { get; set; }
        public string ParentId { get; set;}
        public StatusFlow Flow { get; private set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; private set; }
        public string StatusFlowId { get; set; }
        public string TypeOfGroupOfIssuesId { get; set; }
        public bool IsArchived { get; private set; }

        public void ChangeStatusFlow(StatusFlow newStatusFlow)
        {
            StatusFlowId = newStatusFlow.Id;
            Flow = newStatusFlow;
        }
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