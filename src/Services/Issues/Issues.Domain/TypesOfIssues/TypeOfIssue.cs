using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.DDD.Repositories;
using Issues.Domain.TypesOfIssues.DomainEvents;

namespace Issues.Domain.TypesOfIssues
{
    public class TypeOfIssue : EntityBase, IAggregateRoot
    {
        public TypeOfIssue(string organizationId, string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
            TypesInGroups = new List<TypeOfIssueInTypeOfGroup>();
        }

        public TypeOfIssue()
        {

        }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public bool IsArchived { get; set; }
        public List<TypeOfIssueInTypeOfGroup> TypesInGroups { get; set; }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public TypeOfIssueInTypeOfGroup AddNewTypeOfGroupToCollection(string typeOfGroupId, string statusFlowId)
        {
            if (string.IsNullOrWhiteSpace(typeOfGroupId))
                throw new InvalidOperationException("Given type of group id is empty");

            if (string.IsNullOrWhiteSpace(statusFlowId))
                throw new InvalidOperationException("Given status flow id is empty");

            if (TypesInGroups.Any(d => d.TypeOfGroup.Id == typeOfGroupId))
                throw new InvalidOperationException(
                    $"This type of issue is already added to group with id: {typeOfGroupId}");

            var typeInGroup = new TypeOfIssueInTypeOfGroup(this, statusFlowId, typeOfGroupId);
            TypesInGroups.Add(typeInGroup);
            return typeInGroup;
        }

        public void DeleteTypeOfGroup(string typeOfGroupId)
        {
            var type = TypesInGroups.FirstOrDefault(d => d.TypeOfGroup.Id == typeOfGroupId);
            if (type is null)
                throw new InvalidOperationException($"This type of issue is not assigned to given type of group of issues with id: {typeOfGroupId}");

            TypesInGroups.Remove(type);
            AddDomainEvent(new TypeOfIssueInGroupRemovedDomainEvent(type));
        }

        public void Archive()
        {
            TypesInGroups.ForEach(d => d.Archive());
            IsArchived = true;
        }

        public void UnArchive()
        {
            TypesInGroups.ForEach(d => d.UnArchive());
            IsArchived = false;
        }

    }
}