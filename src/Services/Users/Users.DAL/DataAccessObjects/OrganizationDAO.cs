using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.DAL.DataAccessObjects
{
    public class OrganizationDAO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime TimeOfCreationUtc { get; set; }

        public virtual ICollection<UserDAO> Users { get; set; }
    }
}
