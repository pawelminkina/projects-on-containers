using Issues.API.GrpcServices;
using Issues.Application.Issues.CreateIssue;
using Issues.Domain.Issues;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Infrastructure.Database.Migration;
using Issues.API.Infrastructure.Database.Seeding;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Issues.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddMediatR(
                typeof(IIssueRepository).Assembly, //Domain
                typeof(CreateIssueCommand).Assembly); //Application

            //Database
            services.AddDbContext<IssuesServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"]);
            });
            services.AddDbMigration<IssuesServiceDbContext>();
            services.AddDbSeeding<IssuesServiceDbContext, DefaultIssuesServiceDbSeeder>();

            services.AddScoped<>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapGrpcService<GrpcIssueService>();
                endpoints.MapGrpcService<GrpcGroupOfIssueService>();
                endpoints.MapGrpcService<GrpcStatusFlowService>();
                endpoints.MapGrpcService<GrpcStatusService>();
                endpoints.MapGrpcService<GrpcTypeOfGroupOfIssueService>();
                endpoints.MapGrpcService<GrpcTypeOfIssueService>();
            });
        }
    }
}
