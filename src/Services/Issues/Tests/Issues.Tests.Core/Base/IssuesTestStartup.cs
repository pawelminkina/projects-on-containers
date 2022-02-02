using Issues.API;
using Microsoft.Extensions.Configuration;


namespace Issues.Tests.Core.Base
{
    public class IssuesTestStartup : Startup
    {
        public IssuesTestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
