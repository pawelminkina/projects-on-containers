using Architecture.DDD;
using System;
using System.Linq;
using Architecture.DDD.Exceptions;
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
            return new Issue()
            {
                Id = id,
                Name = name,
                CreatingUserId = creatingUserId,
                _groupOfIssueId = groupOfIssueId,
                TimeOfCreation = timeOfCreation,
                IsDeleted = isDeleted,
                _statusInFlowId = statusInFlowId,
                Content = new IssueContent(textContent)
            };
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
            ValidateModifyOperation();
            IsDeleted = true;
        }

        private void ValidateModifyOperation()
        {
            if (IsDeleted)
                throw new DomainException(ErrorMessages.ModifyOperationFailedBecauseIssueIsDeleted(Id));

            if (GroupOfIssue.IsDeleted)
                throw new DomainException(ErrorMessages.IssueIsInDeleteGroup(GroupOfIssue.Id, Id));
        }

        private void AddContent(string textContent)
        {
            Content = new IssueContent(textContent);
        }

        public static class ErrorMessages
        {
            public static string RequestIssueToModifyIsNotInGivenGroup(string issueId, string groupId) =>
                $"Requested issue to modify with id: {issueId} is not added in group with {groupId}";

            public static string ModifyOperationFailedBecauseIssueIsDeleted(string id) =>
                $"Modify operation failed because issue with Id: {id} is deleted";

            public static string IssueIsInDeleteGroup(string groupId, string issueId) =>
                $"Issue with id: {issueId} is in group with id: {groupId} which is deleted, so it could not be modified";
        }
    }
}
