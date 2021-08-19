using Architecture.DDD;
using System;
using System.Reflection;

namespace Issues.Domain.Issues
{
    public class TypeOfIssues : EntityBase
    {
        internal TypeOfIssues(string organizationId, string name, string statusFlowId, string typeOfGroupOfIssuesId, byte[] icon)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusFlowId = statusFlowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            Icon = icon;
            OrganizationId = organizationId;
        }

        internal TypeOfIssues()
        {

        }
        public virtual string Name { get; protected set; }
        public virtual string StatusFlowId { get; protected set; }
        public virtual string TypeOfGroupOfIssuesId { get; protected set; }
        public virtual byte[] Icon { get; protected set; }
        public virtual string OrganizationId { get; protected set; }
        public virtual bool IsArchived { get; protected set; }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeStatusFlow(string newStatusFlowId) => ChangeStringProperty("StatusFlowId", newStatusFlowId);


    }
}