using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Issues.AcceptanceTests.Base
{
    internal class IssuesTestServer
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(IssuesTestServer)).Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                }).UseStartup<IssuesTestStartup>();

            var testServer = new TestServer(hostBuilder);

            //I need to setup DB
            return testServer;
        }
    }
}
