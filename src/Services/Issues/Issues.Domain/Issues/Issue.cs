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

        private Issue()
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

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");

            if (newName == Name)
                throw new InvalidOperationException("Given new name is the same as current");

            Name = newName;
        }

        public void ChangeStatus(string statusId)
        {
            if (string.IsNullOrWhiteSpace(statusId))
                throw new InvalidOperationException("Given status to change is empty");

            if (statusId == StatusId)
                throw new InvalidOperationException("Given new status id is the same as current");

            StatusId = statusId;
        }

        internal void ChangeGroupOfIssue(string newGroupOfIssueId)
        {
            if (string.IsNullOrWhiteSpace(newGroupOfIssueId))
                throw new InvalidOperationException("Given group of issues id to change is empty");

            if (newGroupOfIssueId == GroupOfIssuesId)
                throw new InvalidOperationException("Given new group of issues is the same as current");

            GroupOfIssuesId = newGroupOfIssueId;
        }

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
