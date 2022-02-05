using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Users.DAL.DataAccessObjects
{
    public class UserDAO : IdentityUser
    {
        public DateTime TimeOfCreationUtc { get; set; }

        public string OrganizationId { get; set; }

        public string Fullname { get; set; }

        public virtual OrganizationDAO Organization { get; set; }
    }
}
