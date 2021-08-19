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
        internal Issue(string name, string statusId, string creatingUserId, string groupOfIssuesId, DateTimeOffset timeOfCreation, string typeOfIssueId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            CreatingUserId = creatingUserId;
            GroupOfIssuesId = groupOfIssuesId;
            TimeOfCreation = timeOfCreation;
            TypeOfIssueId = typeOfIssueId;
        }

        internal Issue()
        {

        }

        public virtual string Name { get; protected set; }
        public virtual string StatusId { get; protected set; }
        public virtual IssueContent Content { get; protected set; }
        public virtual string CreatingUserId { get; protected set; }
        public virtual string GroupOfIssuesId { get; protected set; }
        public virtual DateTimeOffset TimeOfCreation { get; protected set; }
        public virtual bool IsArchived { get; protected set; }
        public virtual string TypeOfIssueId { get; protected set; }

        public IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");
            
            Content = new IssueContent(textContent, this);
            return Content;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeStatus(string statusId) => ChangeStringProperty("StatusId", statusId);

        internal void ChangeGroupOfIssue(string newGroupOfIssueId) => ChangeStringProperty("GroupOfIssuesId", newGroupOfIssueId);


        public void Archive()
        {
            Content.Archive();
            IsArchived = true;
        }

        public void UnArchive()
        {
            Content.UnArchive();
            IsArchived = false;
        }
    }
}
