using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.Issues
{
    public class IssueContent : ValueObjectBase
    {

        public IssueContent(string textContent) : this()
        {
            TextContent = textContent;
            IsArchived = false;
            IsDeleted = false;
        }

        protected IssueContent()
        {

        }
        public virtual string TextContent { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual bool IsDeleted { get; set; }

        public void ChangeTextContent(string newTextContent)
        {
            TextContent = string.IsNullOrWhiteSpace(newTextContent) ? string.Empty : newTextContent;
        }

        internal virtual void Delete()
        {
            IsDeleted = true;
        }
        public virtual void Archive()
        {
            IsArchived = true;
        }

        public virtual void UnArchive()
        {
            IsArchived = false;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return TextContent;
            yield return IsArchived;
        }
    }
}
