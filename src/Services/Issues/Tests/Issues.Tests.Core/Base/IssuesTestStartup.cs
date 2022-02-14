using EventBus;
using EventBus.Abstraction;
using EventBus.InMemory;
using Issues.API;
using Issues.Tests.Core.Auth;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Issues.Tests.Core.Base
{
    public class IssuesTestStartup : Startup
    {
        public IssuesTestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseMiddleware<AutoAuthorizeMiddleware>();
            app.UseAuthorization();
        }

        protected override void ConfigureAuthService(IServiceCollection services)
        {
        }

        protected override void AddEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, InMemoryEventBus>();
        }
    }
}
