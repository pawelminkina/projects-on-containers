using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.API.Infrastructure.Database;
using Users.DAL;
using WebHost.Customization;

namespace Users.Tests.Core.Base
{
    public class UsersTestServer
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(UsersTestServer)).Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("Base/appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                }).UseStartup<UsersTestStartup>()
                .ConfigureLogging(s =>
                {
                    s.AddConsole();
                    s.AddDebug();
                    s.SetMinimumLevel(LogLevel.Trace);
                });

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<UserServiceDbContext>((context, services) =>
                {
                    var logger = services.GetRequiredService<ILogger<DefaultUserServiceDbSeeder>>();
                    var seedService = services.GetRequiredService<IUserSeedItemService>();
                    new DefaultUserServiceDbSeeder()
                        .SeedAsync(context, seedService, logger, true)
                        .Wait();
                });

            return testServer;
        }

        public GrpcChannel GetGrpcChannel(TestServer server)
        {
            var client = server.CreateClient();
            var channel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions()
            {
                HttpClient = client
            });
            return channel;
        }
    }
}
