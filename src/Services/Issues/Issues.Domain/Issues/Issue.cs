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
        public Issue(string name, string statusId, string creatingUserId, string groupOfIssuesId, DateTimeOffset timeOfCreation)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            CreatingUserId = creatingUserId;
            GroupOfIssuesId = groupOfIssuesId;
            TimeOfCreation = timeOfCreation;
        }

        public Issue()
        {

        }

        public string Name { get; }
        public string StatusId { get; }
        public IssueContent Content { get; set; }
        public string CreatingUserId { get; }
        public string GroupOfIssuesId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }


        public IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");
            
            Content = new IssueContent(textContent, this);
            return Content;
        }

        public void DeleteContent()
        {
            Content = null;
        }
    }
}
