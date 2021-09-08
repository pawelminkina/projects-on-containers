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
        public void Archive()
        {
            _groups.ForEach(d=>d.Archive());
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
