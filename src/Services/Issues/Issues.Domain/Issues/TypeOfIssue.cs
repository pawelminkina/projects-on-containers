using Architecture.DDD;
using System;
using System.Reflection;

namespace Issues.Domain.Issues
{
    public class TypeOfIssue : EntityBase
    {
        public TypeOfIssue(string organizationId, string name, string statusFlowId, string typeOfGroupOfIssuesId, byte[] icon)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusFlowId = statusFlowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            Icon = icon;
            OrganizationId = organizationId;
            IsArchived = false;
        }

        public TypeOfIssue()
        {

        }
        public virtual string Name { get; set; }
        public virtual string StatusFlowId { get; set; }
        public virtual string TypeOfGroupOfIssuesId { get; set; }
        public virtual byte[] Icon { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual bool IsArchived { get; set; }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

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