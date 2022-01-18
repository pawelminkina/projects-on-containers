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
            IsDefault = false;
        }

        public TypeOfGroupOfIssues()
        {
            _groups = new List<GroupOfIssues>();
        }

        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public bool IsDefault { get; set; }

        protected readonly List<GroupOfIssues> _groups;
        public IReadOnlyCollection<GroupOfIssues> Groups => _groups;

        public void RenameTypeOfGroup(string newName)
        {
            if (IsDefault)
                throw new InvalidOperationException("You can't change name of default type of group of issues");
            
            ChangeStringProperty("Name", newName);
        }

        public GroupOfIssues AddNewGroupOfIssues(string name, string shortName)
        {
            if (shortName.Length is > GroupOfIssues.MaxShortNameLength or < GroupOfIssues.MinShortNameLength)
                throw new InvalidOperationException($"Requested new short name: {shortName} have more cases then {GroupOfIssues.MaxShortNameLength} or has less cases then {GroupOfIssues.MinShortNameLength}");
            
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Requested group of issues name is empty");

            var group = new GroupOfIssues(name, shortName, this);
            _groups.Add(group);

            AddDomainEvent(new GroupOfIssuesCreatedDomainEvent(group));
            return group;
        }

        public void DeleteGroupOfIssues(string id)
        {
            var groupToDelete = _groups.FirstOrDefault(s => s.Id == id);
            if (groupToDelete is null)
                throw new InvalidOperationException($"Requested group with id: {id} don't exist in type of group of issues with id: {Id}");

            if (groupToDelete.IsDeleted)
                throw new InvalidOperationException("Cannot delete group which is already deleted");

            groupToDelete.Delete();
        }

        public bool CanBeDeleted()
        {
            if (IsDefault)
                return false;

            if (Groups.Any())
                return false;

            return true;
        }
    }
}
