using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        public Issue(string name, string statusId, IssueContent content, string creatingUserId, string groupOfIssuesId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            Content = content;
            CreatingUserId = creatingUserId;
            GroupOfIssuesId = groupOfIssuesId;
        }

        public Issue()
        {

        }

        public string Name { get; }
        public string StatusId { get; }
        public virtual IssueContent Content { get; }
        public string CreatingUserId { get; }
        public string GroupOfIssuesId { get; set; }
    }
}
