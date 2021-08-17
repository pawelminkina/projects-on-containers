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
        public string TextContent { get; protected set; }
        public Issue ParentIssue { get; protected set; }
        public bool IsArchived { get; private set; }

        public void ChangeTextContent(string newTextContent)
        {
            TextContent = string.IsNullOrWhiteSpace(newTextContent) ? string.Empty : newTextContent;
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
