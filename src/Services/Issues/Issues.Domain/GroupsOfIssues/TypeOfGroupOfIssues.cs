using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] //For moq purpose 

namespace Issues.Domain.GroupsOfIssues
{

    public class TypeOfGroupOfIssues : EntityBase, IAggregateRoot
    {
        public TypeOfGroupOfIssues(string organizationId, string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
        }

        public TypeOfGroupOfIssues()
        {

        }

        public virtual string Name { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual List<GroupOfIssues> Groups { get; set; }
        public void RenameGroup(string newName)=> ChangeStringProperty("Name", newName);

        public GroupOfIssues AddNewGroupOfIssues(string name)
        {
            var group = new GroupOfIssues(name, this);
            Groups.Add(group);
            return group;
        }
        public void Archive(ITypeGroupOfIssuesArchivePolicy policy)
        {
            IsArchived = true;
            policy.Archive(this);
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
