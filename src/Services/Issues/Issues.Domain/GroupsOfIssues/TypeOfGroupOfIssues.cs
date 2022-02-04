using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Architecture.DDD.Exceptions;
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
            AddDomainEvent(new TypeOfGroupOfIssuesCreatedDomainEvent(this));
        }

        public static TypeOfGroupOfIssues CreateDefault(string organizationId, string name)
        {
            var typeOfGroupOfIssues = new TypeOfGroupOfIssues(organizationId, name);
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
        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsDefault { get; protected set; }

        protected readonly List<GroupOfIssues> _groups;
        public IReadOnlyCollection<GroupOfIssues> Groups => _groups;

        public void RenameTypeOfGroup(string newName)
        {
            if (IsDefault)
                throw new InvalidOperationException(ErrorMessages.CannotChangeNameOfDefaultTypeOfGroupOfIssuesWithId(Id));

            ChangeStringProperty("Name", newName);

            AddDomainEvent(new TypeOfGroupOfIssuesRenamedDomainEvent(this));
        }

        public GroupOfIssues AddNewGroupOfIssues(string name, string shortName)
        {
            if (!GroupOfIssues.FitsShortNameSize(shortName))
                throw new DomainException(GroupOfIssues.ErrorMessages.RequestedShortNameHasNotRequiredSize(shortName));
            
            if (string.IsNullOrEmpty(name))
                throw new DomainException(ErrorMessages.RequestedGroupOfIssuesNameIsEmpty());

            var group = new GroupOfIssues(name, shortName, this);
            _groups.Add(group);

            AddDomainEvent(new GroupOfIssuesCreatedDomainEvent(group));
            return group;
        }

        public void DeleteGroupOfIssues(string id)
        {
            var groupToDelete = _groups.FirstOrDefault(s => s.Id == id);
            if (groupToDelete is null)
                throw new DomainException(ErrorMessages.GroupToDeleteIsNotInThisTypeOfGroup(id,this.Id));

            if (groupToDelete.IsDeleted)
                throw new DomainException(ErrorMessages.CannotDeleteGroupWhichIsAlreadyDeleted(id));

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
                reason = ErrorMessages.CanNotBeDeletedBecauseIsDefault(Id);
                return false;
            }

            if (Groups.Any())
            {
                reason = ErrorMessages.CanNotBeDeletedBecauseHasGroupsAssigned(Id);
                return false;
            }

            return true;
        }

        public static class ErrorMessages
        {
            public static string CanNotBeDeletedBecauseIsDefault(string id) =>
                $"Type of group of issues with id: {id} could not be deleted because it is default";

            public static string CanNotBeDeletedBecauseHasGroupsAssigned(string id) =>
                $"Type of group of issues with id: {id} could not be deleted because it has groups assigned to it";

            public static string SomeTypeOfGroupAlreadyExistWithName(string name) =>
                $"Type of group of issues with name: {name} already exist";

            public static string GroupToDeleteIsNotInThisTypeOfGroup(string idToDelete, string idOfType) =>
                $"Requested group with id: {idToDelete} don't exist in type of group of issues with id: {idOfType}";

            public static string CannotDeleteGroupWhichIsAlreadyDeleted(string idOfGroup) =>
                $"Cannot delete group with id: {idOfGroup} which is already deleted";

            public static string CannotChangeNameOfDefaultTypeOfGroupOfIssuesWithId(string id) =>
                $"You can't change name of default type of group of issues with {id}";

            public static string RequestedGroupOfIssuesNameIsEmpty() =>
                "Requested group of issues name is empty";
        }
    }
}
