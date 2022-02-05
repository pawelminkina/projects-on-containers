using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;

namespace User.Core.Domain
{
    public class User : EntityBase
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Fullname { get; set; }

        public DateTimeOffset TimeOfCreation { get; set; }

        public Organization Organization { get; set; }
    }
}
