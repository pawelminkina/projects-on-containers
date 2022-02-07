using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Users.API;

namespace Users.Tests.Core.Base
{
    public class UsersTestStartup : Startup
    {
        public UsersTestStartup(IConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
