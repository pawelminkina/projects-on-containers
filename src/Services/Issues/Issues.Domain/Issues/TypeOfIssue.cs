using System;
using Architecture.DDD;

namespace Issues.Domain.Issues
{
    public class TypeOfIssue : EntityBase
    {
        internal TypeOfIssue(string organizationId, string name, string flowId, string typeOfGroupOfIssuesId, byte[] icon)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            FlowId = flowId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            Icon = icon;
            OrganizationId = organizationId;
        }

        private TypeOfIssue()
        {
                
        }
        public virtual string Name { get; protected set; }
        public virtual string FlowId { get; protected set; }
        public virtual string TypeOfGroupOfIssuesId { get; protected set; }
        public virtual byte[] Icon { get; protected set; }
        public virtual string OrganizationId { get; protected set; }
        public virtual bool IsArchived { get; protected set; }

    }
}