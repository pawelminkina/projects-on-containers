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

        public IssueContent(string textContent, Issue parentIssue)
        {
            Id = Guid.NewGuid().ToString();
            ParentIssue = parentIssue;
            TextContent = textContent;
        }

        public IssueContent()
        {

        }
        public string TextContent { get; }
        public Issue ParentIssue { get; }
    }
}
