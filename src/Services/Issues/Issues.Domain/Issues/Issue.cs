using Architecture.DDD;
using System;
using Issues.Domain.GroupsOfIssues;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        public Issue(string name, string creatingUserId, GroupOfIssues groupOfIssue, DateTimeOffset timeOfCreation, string textContent) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            CreatingUserId = creatingUserId;
            GroupOfIssue = groupOfIssue;
            GroupOfIssueId = groupOfIssue.Id;
            TimeOfCreation = timeOfCreation;
            IsDeleted = false;
            AddContent(textContent);
        }

        public Issue()
        {

        }

        public string Name { get; set; }
        public string CreatingUserId { get; set; }
        public IssueContent Content { get; set; }
        public GroupOfIssues GroupOfIssue { get; set; }
        public string GroupOfIssueId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public bool IsDeleted { get; set; }

        private IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");

            Content = new IssueContent(textContent);
            return Content;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
