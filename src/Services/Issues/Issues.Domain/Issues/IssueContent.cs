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
        public string TextContent { get; private set; }
        public bool IsArchived { get; private set; }
        public bool IsDeleted { get; private set; }

        public void ChangeTextContent(string newTextContent)
        {
            TextContent = string.IsNullOrWhiteSpace(newTextContent) ? string.Empty : newTextContent;
        }

        internal void Delete()
        {
            IsDeleted = true;
        }
        public void Archive()
        {
            IsArchived = true;
        }

        public void UnArchive()
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
