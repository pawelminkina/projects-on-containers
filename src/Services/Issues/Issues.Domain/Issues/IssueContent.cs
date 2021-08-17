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

        internal IssueContent(string textContent, Issue parentIssue)
        {
            Id = Guid.NewGuid().ToString();
            ParentIssue = parentIssue;
            TextContent = textContent;
        }

        private IssueContent()
        {

        }
        public string TextContent { get; private set; }
        public Issue ParentIssue { get; private set; }

        public void ChangeTextContent(string newTextContent)
        {
            TextContent = newTextContent ?? string.Empty;
        }
    }
}
