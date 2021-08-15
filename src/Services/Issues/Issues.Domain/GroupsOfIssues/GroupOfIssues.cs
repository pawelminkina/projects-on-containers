using Architecture.DDD;
using Issues.Domain.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<Issue> Issues { get; set; }
    }
}
