using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues.DomainEvents;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] //For moq purpose 

namespace Issues.Domain.GroupsOfIssues
{

    public class TypeOfGroupOfIssues : EntityBase, IAggregateRoot
    {
        public TypeOfGroupOfIssues(string organizationId, string name) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
            IsDefault = false;
        }

        protected TypeOfGroupOfIssues()
        {
            _groups = new List<GroupOfIssues>();
        }

        public string Name { get; private set; }
        public string OrganizationId { get; private set; }
        public bool IsArchived { get; private set; }
        public bool IsDefault { get; private set; }

        protected readonly List<GroupOfIssues> _groups;
        public IReadOnlyCollection<GroupOfIssues> Groups => _groups;

        public void RenameGroup(string newName)=> ChangeStringProperty("Name", newName);

        public GroupOfIssues AddNewGroupOfIssues(string name, string shortName)
        {
            if (shortName.Length is > GroupOfIssues.MaxShortNameLength or < GroupOfIssues.MinShortNameLength)
                throw new InvalidOperationException($"Requested new short name: {shortName} have more cases then {GroupOfIssues.MaxShortNameLength} or has less cases then {GroupOfIssues.MinShortNameLength}");
            
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Requested group of issues name is empty");

            var group = new GroupOfIssues(name, shortName, this);
            _groups.Add(group);
            return group;
        }

        public void AddExistingGroupsOfIssues(List<GroupOfIssues> groups)
        {
            foreach (var group in groups)
                group.ChangeTypeOfGroupOfIssues(this);

            _groups.AddRange(groups);
        }

        public void RemoveGroupAndMoveIssuesToAnotherGroup(string idOfGroupToDelete, string idOfGroupToWhichIssuesShouldBeMoved)
        {
            var toDelete = _groups.FirstOrDefault(d => d.Id == idOfGroupToDelete);

            if (toDelete is null)
                throw new InvalidOperationException($"Requested group to delete is not in type with id: {Id}");

            var toWhichShouldBeMoved = _groups.FirstOrDefault(s => s.Id == idOfGroupToWhichIssuesShouldBeMoved);

            if (toWhichShouldBeMoved is null)
                throw new InvalidOperationException($"Requested group to which should be moved is not in type with id: {Id}");

            foreach (var issue in toDelete.Issues)
                issue.ChangeGroupOfIssue(toWhichShouldBeMoved);

            _groups.Remove(toDelete); //TODO question: will it delete it from db, or do i need domain event which will remove it from db
        }

        public void SetIsDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new TypeOfGroupOfIssuesSettedToDefaultDomainEvent(this));
        }

        public void SetIsDefaultToFalse()
        {
            IsDefault = false;
            AddDomainEvent(new TypeOfGroupOfIssuesUnsettedFromDefaultDomainEvent(this));
        }

        public void ArchiveAndMoveGroups(TypeOfGroupOfIssues typeWhereGroupsWillBeMoved)
        {
            if (IsDefault)
                throw new InvalidOperationException("Default type of group of issues could not be archived");

            if (typeWhereGroupsWillBeMoved is null)
                throw new InvalidOperationException("Requested type where groups will be moved is null");

            IsArchived = true;
            AddDomainEvent(new TypeOfGroupOfIssuesArchivedDomainEvent(this, typeWhereGroupsWillBeMoved)); 
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
