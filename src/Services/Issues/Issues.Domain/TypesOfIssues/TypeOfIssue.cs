using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Reflection;
using Architecture.DDD.Repositories;

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
        }

        public TypeOfIssue()
        {

        }
        public virtual string Name { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual List<TypeOfIssueInTypeOfGroup> TypesInGroups { get; set; }

        public void Rename(string newName) => ChangeStringProperty("Name", newName);

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