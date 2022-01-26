using Architecture.DDD;
using System;
using System.Linq;
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
            StatusInFlowId = groupOfIssue.ConnectedStatusFlow.StatusesInFlow.First(s => s.IsDefault).Id;
        }

        public Issue()
        {

        }

        internal static Issue CreateWholeObject(string id, string name, string creatingUserId, string groupOfIssueId, DateTimeOffset timeOfCreation, string textContent, bool isDeleted, string statusInFlowId)
        {
            return null;
        }

        public string Name { get; set; }
        public string CreatingUserId { get; set; }
        public IssueContent Content { get; set; }
        public GroupOfIssues GroupOfIssue { get; set; }
        public string GroupOfIssueId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusInFlowId { get; set; }
        private IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");

            Content = new IssueContent(textContent);
            return Content;
        }

        public void ChangeTextContent(string newTextContent)
        {
            ValidateModifyOperation();
            Content.ChangeTextContent(newTextContent);
        }

        public void Rename(string newName)
        {
            ValidateModifyOperation();
            ChangeStringProperty("Name", newName);

        }

        internal void SetIsDeletedToTrue()
        {
            IsDeleted = true;
        }

        private void ValidateModifyOperation()
        {
            if (IsDeleted)
                throw new InvalidOperationException($"Issue with id: {Id} is already deleted so it could not be modified");

            if (GroupOfIssue.IsDeleted)
                throw new InvalidOperationException($"Issue with id: {Id} is in group which is deleted, so it could not be modified");

        }
    }
}
