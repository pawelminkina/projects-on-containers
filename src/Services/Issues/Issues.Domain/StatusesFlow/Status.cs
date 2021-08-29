using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;

namespace Issues.Domain.StatusesFlow
{
    public class Status : EntityBase, IAggregateRoot
    {
        public Status(string name, string organizationId) : this()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OrganizationId = organizationId;
            IsArchived = false;
        }
        protected Status()
        {

        }
        public string Name { get; private set; }
        public string OrganizationId { get; private set; }
        public bool IsArchived { get; private set; }
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
