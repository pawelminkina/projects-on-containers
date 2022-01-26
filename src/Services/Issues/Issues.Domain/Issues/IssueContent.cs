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

        internal IssueContent(string textContent) : this()
        {
            TextContent = textContent;
        }

        protected IssueContent()
        {

        }
        public string TextContent { get; private set; }

        internal void ChangeTextContent(string newTextContent)
        {
            TextContent = string.IsNullOrWhiteSpace(newTextContent) ? string.Empty : newTextContent;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return TextContent;
        }
    }
}
