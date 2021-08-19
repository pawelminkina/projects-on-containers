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
        internal Status(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
        }
        internal Status()
        {

        }
        public virtual string Name { get; protected set; }
        public virtual string OrganizationId { get; protected set; }
        public virtual bool IsArchived { get; protected set; }
        public void Rename(string newName) => ChangeStringProperty("Name", newName);

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
