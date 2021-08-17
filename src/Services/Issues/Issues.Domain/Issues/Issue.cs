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

        public string Name { get; protected set; }
        public string StatusId { get; protected set; }
        public IssueContent Content { get; protected set; }
        public string CreatingUserId { get; protected set; }
        public string GroupOfIssuesId { get; protected set; }
        public DateTimeOffset TimeOfCreation { get; protected set; }
        public bool IsArchived { get; private set; }

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
