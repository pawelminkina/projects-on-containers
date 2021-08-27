using Architecture.DDD;
using System;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.TypesOfIssues;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        public Issue(string name, string statusId, string creatingUserId, GroupOfIssues groupOfIssue, DateTimeOffset timeOfCreation, string typeOfIssueId, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            CreatingUserId = creatingUserId;
            GroupOfIssue = groupOfIssue;
            TimeOfCreation = timeOfCreation;
            TypeOfIssueId = typeOfIssueId;
            OrganizationId = organizationId;
            IsArchived = false;
        }

        public Issue()
        {

        }

        public virtual string Name { get; set; }
        public virtual string StatusId { get; set; }
        public virtual IssueContent Content { get; set; }
        public virtual string CreatingUserId { get; set; }
        public virtual GroupOfIssues GroupOfIssue { get; set; }
        public virtual DateTimeOffset TimeOfCreation { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual string TypeOfIssueId { get; set; }
        public virtual string OrganizationId { get; }
        public virtual TypeOfIssue TypeOfIssue { get; set; }

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
        internal void ChangeGroupOfIssue(GroupOfIssues newGroup)
        {
            if (newGroup == null)
                throw new InvalidOperationException("Given group issue to set is null");
            GroupOfIssue = newGroup;
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
