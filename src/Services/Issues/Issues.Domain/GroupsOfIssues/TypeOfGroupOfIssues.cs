using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Domain.GroupsOfIssues.DomainEvents;
using Issues.Domain.StatusesFlow.DomainEvents;

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

        public static TypeOfGroupOfIssues CreateDefault(string name, string organizationId)
        {
            var typeOfGroupOfIssues = new TypeOfGroupOfIssues()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                OrganizationId = organizationId,
            };
            typeOfGroupOfIssues.SetDefaultToTrue();
            return typeOfGroupOfIssues;
        }

        internal static TypeOfGroupOfIssues CreateWholeObject(string id, string name, string organizationId, bool isDefault)
        {
            return new TypeOfGroupOfIssues()
            {
                Id = id,
                IsDefault = isDefault,
                Name = name,
                OrganizationId = organizationId
            };
        }

        protected TypeOfGroupOfIssues()
        {
            _groups = new List<GroupOfIssues>();
            AddDomainEvent(new TypeOfGroupOfIssuesCreatedDomainEvent(this));
        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsDefault { get; protected set; }

        protected readonly List<GroupOfIssues> _groups;
        public IReadOnlyCollection<GroupOfIssues> Groups => _groups;

        public void RenameTypeOfGroup(string newName)
        {
            if (IsDefault)
                throw new InvalidOperationException("You can't change name of default type of group of issues");

            ChangeStringProperty("Name", newName);

            AddDomainEvent(new TypeOfGroupOfIssuesRenamedDomainEvent(this));
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
                throw new InvalidOperationException($"Cannot delete group with id: {id} which is already deleted");

            groupToDelete.Delete();
        }

        private void SetDefaultToTrue()
        {
            IsDefault = true;
            AddDomainEvent(new DefaultPropertyInTypeOfGroupOfIssuesChangedToTrueDomainEvent(this));
        }

        public bool CanBeDeleted(out string reason)
        {
            reason = string.Empty;
            
            if (IsDefault)
            {
                reason = $"Type of group of issues with id: {Id} could not be deleted because it is default";
                return false;
            }

            if (Groups.Any())
            {
                reason = $"Type of group of issues with id: {Id} could not be deleted because it has groups assigned to it";
                return false;
            }

            return true;
        }
    }
}
