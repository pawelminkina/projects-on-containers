using Issues.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Issues.Tests.Core.Base
{
    public class IssuesTestStartup : Startup
    {
        public IssuesTestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void AddEventBus(IServiceCollection services)
        {
            
        }
    }
}
