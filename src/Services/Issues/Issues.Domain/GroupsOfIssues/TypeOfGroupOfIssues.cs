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
        public TypeOfGroupOfIssues(string organizationId, string name) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
        }

        protected TypeOfGroupOfIssues()
        {
            _groups = new List<GroupOfIssues>();
        }

        public string Name { get; private set; }
        public string OrganizationId { get; private set; }
        public bool IsArchived { get; private set; }

        protected readonly List<GroupOfIssues> _groups;
        public IReadOnlyCollection<GroupOfIssues> Groups => _groups;

        public void RenameGroup(string newName)=> ChangeStringProperty("Name", newName);

        public GroupOfIssues AddNewGroupOfIssues(string name)
        {
            var group = new GroupOfIssues(name, this);
            _groups.Add(group);
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
