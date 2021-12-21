using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.API;
using Microsoft.Extensions.Configuration;

namespace Issues.AcceptanceTests.Base
{
    public class IssuesTestStartup : Startup
    {
        public IssuesTestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
