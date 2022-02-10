using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Models
{
    public class POCUser
    {
        public POCUser(string id, string organizationId, string username, DateTimeOffset timeOfCreation)
        {
            Id = id;
            OrganizationId = organizationId;
            Username = username;
            TimeOfCreation = timeOfCreation;
        }

        public string Id { get; set; }

        public string OrganizationId { get; set; }

        public string Username { get; set; }

        public DateTimeOffset TimeOfCreation { get; set; }
    }
}
