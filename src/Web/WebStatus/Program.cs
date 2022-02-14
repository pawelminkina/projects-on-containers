using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                    loggerConfiguration.Enrich.WithProperty("ApplicationContext", "WebStatus");
                    loggerConfiguration.Enrich.FromLogContext();
                    loggerConfiguration.WriteTo.Console();
                    loggerConfiguration.WriteTo.Seq(hostingContext.Configuration["Serilog:SeqServerUrl"]);
                    loggerConfiguration.WriteTo.Http(hostingContext.Configuration["Serilog:LogStashUrl"]);
                });
    }
}
