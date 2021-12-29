using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Issues.API.Infrastructure.Database.Seeding;
using Issues.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using WebHost.Customization;

namespace Issues.AcceptanceTests.Base
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
                .UseSerilog((c, s) =>
                {
                    s.MinimumLevel.Verbose();
                    s.Enrich.FromLogContext();
                    s.WriteTo.Console();
                    s.ReadFrom.Configuration(c.Configuration);
                    
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
