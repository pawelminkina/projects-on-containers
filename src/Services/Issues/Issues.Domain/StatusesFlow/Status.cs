using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;

namespace Issues.Domain.StatusesFlow
{
    public class Status : EntityBase, IAggregateRoot
    {
        public Status(string name, string organizationId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
        }
        public Status()
        {

        }
        public virtual string Name { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual bool IsArchived { get; set; }
        public void Rename(string newName) => ChangeStringProperty("Name", newName);

        public void Archive()
        {
            IsArchived = true;
        }

        public void UnArchive()
        {
            IsArchived = false;
        }
    }
}
