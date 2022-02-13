using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus;
using EventBus.Abstraction;
using EventBus.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API;

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
    }
}
