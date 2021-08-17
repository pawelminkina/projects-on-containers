using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleToAttribute("Issues.Tests")]

namespace Issues.Domain.GroupsOfIssues
{
    public class TypeOfGroupOfIssues : EntityBase
    {
        internal TypeOfGroupOfIssues(string organizationId, string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }

        internal TypeOfGroupOfIssues()
        {

        }

        public virtual string Name { get; protected set; }
        public virtual string OrganizationId { get; protected set; }
        public virtual bool IsArchived { get; set; }

        public void RenameGroup(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");

            if (newName == Name)
                throw new InvalidOperationException("Requested new name is the same as current");

            Name = newName;
        }

        public void Archive(ITypeGroupOfIssuesArchivePolicy policy)
        {
            IsArchived = true;
            policy.Archive();
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
