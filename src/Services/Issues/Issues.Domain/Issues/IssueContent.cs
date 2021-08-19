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

        internal IssueContent()
        {

        }
        public virtual string TextContent { get; protected set; }
        public virtual Issue ParentIssue { get; protected set; }
        public virtual bool IsArchived { get; protected set; }

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
