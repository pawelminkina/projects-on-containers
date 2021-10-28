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
            _statusFlowId = statusFlowId;
            _typeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            IsArchived = false;
        }

        protected TypeOfIssueInTypeOfGroup()
        {
            
        }
        public TypeOfIssue Parent { get; private set; }
        public StatusFlow Flow { get; private set; }
        public TypeOfGroupOfIssues TypeOfGroup { get; private set; }
        private string _statusFlowId;
        private string _typeOfGroupOfIssuesId;
        public bool IsArchived { get; private set; }

        public void ChangeStatusFlow(StatusFlow newStatusFlow)
        {
            _statusFlowId = newStatusFlow.Id;
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