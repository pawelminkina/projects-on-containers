using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;

namespace Users.Core.Domain
{
    public class Organization : EntityBase
    {
        public string Name { get; set; }

        public DateTimeOffset TimeOfCreation { get; set; }
    }
}
