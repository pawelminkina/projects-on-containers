using System.Reflection;
using Grpc.Net.Client;
using Issues.API.Infrastructure.Database.Seeding;
using Issues.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebHost.Customization;

namespace Issues.Tests.Core.Base
{
    public class IssuesTestServer
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(IssuesTestServer)).Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("Base/appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                }).UseStartup<IssuesTestStartup>()
                .ConfigureLogging(s =>
                {
                    s.AddConsole();
                    s.AddDebug();
                    s.SetMinimumLevel(LogLevel.Trace);
                });

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<IssuesServiceDbContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<IssuesServiceDbSeed>>();
                    var seedService = services.GetService<IIssueSeedItemService>();
                    var options = services.GetService<IOptions<IssueServiceSeedingOptions>>();

                    new IssuesServiceDbSeed()
                        .SeedAsync(context, env, logger, seedService, options.Value, true)
                        .Wait();
                });

            //I need to setup DB
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
