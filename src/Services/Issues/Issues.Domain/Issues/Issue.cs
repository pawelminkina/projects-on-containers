using Architecture.DDD;
using System;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.TypesOfIssues;

namespace Issues.Domain.Issues
{
    public class Issue : EntityBase
    {
        public Issue(string name, string statusId, string creatingUserId, GroupOfIssues groupOfIssue, DateTimeOffset timeOfCreation, string typeOfIssueId) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            StatusId = statusId;
            CreatingUserId = creatingUserId;
            GroupOfIssue = groupOfIssue;
            GroupOfIssueId = groupOfIssue.Id;
            TimeOfCreation = timeOfCreation;
            TypeOfIssueId = typeOfIssueId;
            IsArchived = false;
            IsDeleted = false;
        }

        public Issue()
        {

        }

        public string Name { get; set; }
        public string StatusId { get; set; }
        public string CreatingUserId { get; set; }
        public IssueContent Content { get; set; }
        public GroupOfIssues GroupOfIssue { get; set; }
        public string GroupOfIssueId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }

        private string TypeOfIssueId; //https://github.com/dotnet-architecture/eShopOnContainers/blob/71994d0ad88d51f758d8124b16bddf944cc7d91b/src/Services/Ordering/Ordering.Infrastructure/EntityConfigurations/OrderEntityTypeConfiguration.cs
        public TypeOfIssue TypeOfIssue { get; private set; }

        public IssueContent AddContent(string textContent)
        {
            if (Content != null)
                throw new InvalidOperationException($"Content to issue with id: {Id} is already added");

            Content = new IssueContent(textContent);
            return Content;
        }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void ChangeStatus(string statusId) => ChangeStringProperty("StatusId", statusId);

        //TODO should be tested with integration test
        internal void ChangeGroupOfIssue(GroupOfIssues newGroup)
        {
            if (newGroup == null)
                throw new InvalidOperationException("Given group issue to set is null");
            
            GroupOfIssue.RemoveIssueFromGroup(Id);
            
            GroupOfIssue = newGroup;
        }

        public void Delete()
        {
            Content.Delete();
            IsDeleted = true;
        }

        internal void Archive()
        {
            Content.Archive();
            IsArchived = true;
        }

        internal void UnArchive()
        {
            Content.UnArchive();
            IsArchived = false;
        }
    }
}
