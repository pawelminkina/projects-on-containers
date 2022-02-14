using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.CQRS.Organizations.Commands.AddOrganization
{
    public class AddOrganizationResponse 
    {
        public string OrganizationId { get; set; }
        public string DefaultUserName { get; set; }
        public string DefaultUserPassword { get; set; }
    }
}
