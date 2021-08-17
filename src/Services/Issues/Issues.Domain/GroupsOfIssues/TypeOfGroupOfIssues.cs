using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public class TypeOfGroupOfIssues : EntityBase
    {
        internal TypeOfGroupOfIssues(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }

        private TypeOfGroupOfIssues()
        {

        }

        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsArchived { get; private set; }

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
