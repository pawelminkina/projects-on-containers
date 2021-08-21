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
            ParentIssueId = parentIssue.Id;
            TextContent = textContent;
            IsArchived = false;
        }

        public IssueContent()
        {

        }
        public virtual string TextContent { get; set; }
        public virtual Issue ParentIssue { get; set; }
        public virtual string ParentIssueId { get; set; }
        public virtual bool IsArchived { get; set; }

        public void ChangeTextContent(string newTextContent)
        {
            TextContent = string.IsNullOrWhiteSpace(newTextContent) ? string.Empty : newTextContent;
        }

        public virtual void Archive()
        {
            IsArchived = true;
        }

        public virtual void UnArchive()
        {
            IsArchived = false;
        }
    }
}
