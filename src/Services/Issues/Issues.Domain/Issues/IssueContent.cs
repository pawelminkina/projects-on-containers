using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.Issues
{
    public class IssueContent : EntityBase
    {

        public IssueContent(string issueId, string textContent)
        {
            Id = Guid.NewGuid().ToString();
            IssueId = issueId;
            TextContent = textContent;
        }

        public IssueContent()
        {

        }
        public string TextContent { get; }
#warning instead of foreignkey i should use fluent api from dbcontext
        [ForeignKey("Issue")]
    public string IssueId { get; }
    }
}
