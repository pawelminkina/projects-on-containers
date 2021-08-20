using Architecture.DDD;
using System;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        public Issue(string name, string statusId, string creatingUserId, string groupOfIssuesId, DateTimeOffset timeOfCreation, string typeOfIssueId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            CreatingUserId = creatingUserId;
            GroupOfIssuesId = groupOfIssuesId;
            TimeOfCreation = timeOfCreation;
            TypeOfIssueId = typeOfIssueId;
        }

        public Issue()
        {

        }

        public virtual string Name { get; set; }
        public virtual string StatusId { get; set; }
        public virtual IssueContent Content { get; set; }
        public virtual string CreatingUserId { get; set; }
        public virtual string GroupOfIssuesId { get; set; }
        public virtual DateTimeOffset TimeOfCreation { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual string TypeOfIssueId { get; set; }

        public IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");

            Content = new IssueContent(textContent, this);
            return Content;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeStatus(string statusId) => ChangeStringProperty("StatusId", statusId);

        //TODO should be tested with integration test
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
