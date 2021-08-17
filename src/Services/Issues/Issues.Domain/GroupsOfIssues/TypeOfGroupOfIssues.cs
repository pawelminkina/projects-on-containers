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

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");
            Name = newName;
        }
    }
}
