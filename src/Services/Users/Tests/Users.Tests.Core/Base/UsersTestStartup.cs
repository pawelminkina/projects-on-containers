using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus;
using EventBus.Abstraction;
using EventBus.InMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API;
using Users.Tests.Core.Auth;

namespace Users.Tests.Core.Base
{
    public class UsersTestStartup : Startup
    {
        public UsersTestStartup(IConfiguration configuration) : base(configuration)
        {
            
        }

        protected override void AddEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, InMemoryEventBus>();
        }

        protected override void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseMiddleware<AutoAuthorizeMiddleware>();
            app.UseAuthorization();
        }

        protected override void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthentication();
        }

        protected override void AddHealthChecks(IServiceCollection services)
        {
            
        }

        protected override void MapHealthChecks(IEndpointRouteBuilder endpoints)
        {
        }
    }
}
