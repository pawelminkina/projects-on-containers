using Architecture.DDD;
using System;
using System.Linq;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        internal Issue(string name, string creatingUserId, GroupOfIssues groupOfIssue, DateTimeOffset timeOfCreation, string textContent) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            CreatingUserId = creatingUserId;
            GroupOfIssue = groupOfIssue;
            _groupOfIssueId = groupOfIssue.Id;
            TimeOfCreation = timeOfCreation;
            IsDeleted = false;
            AddContent(textContent);
            _statusInFlowId = groupOfIssue.ConnectedStatusFlow.StatusesInFlow.First(s => s.IsDefault).Id;
            StatusInFlow = groupOfIssue.ConnectedStatusFlow.StatusesInFlow.First(s => s.IsDefault);
        }

        protected Issue()
        {

        }

        internal static Issue CreateWholeObject(string id, string name, string creatingUserId, string groupOfIssueId, DateTimeOffset timeOfCreation, string textContent, bool isDeleted, string statusInFlowId)
        {
            return null;
        }

        public string Name { get; protected set; }
        public string CreatingUserId { get; protected set; }
        public IssueContent Content { get; protected set; }

        private string _groupOfIssueId;
        public GroupOfIssues GroupOfIssue { get; protected set; }
        public DateTimeOffset TimeOfCreation { get; protected set; }
        public bool IsDeleted { get; protected set; }

        private string _statusInFlowId;
        public StatusInFlow StatusInFlow { get; protected set; }
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
