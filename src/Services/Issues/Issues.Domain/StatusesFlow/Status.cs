using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public class Status : EntityBase
    {
        public Status(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }
        public Status()
        {

        }
        public string Name { get; protected set; }
        public string OrganizationId { get; protected set; }
        public bool IsArchived { get; private set; }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidOperationException("Given name to change is empty");

            if (Name == newName)
                throw new InvalidOperationException("Given new name of status is the same as current");
            Name = newName;
        }

        public void Archive(IStatusArchivePolicy policy)
        {
            policy.Archive();
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
