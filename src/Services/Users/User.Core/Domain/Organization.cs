using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;

namespace User.Core.Domain
{
    public class Organization : EntityBase
    {
        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset TimeOfCreation { get; set; }
    }
}
