using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Services.Issues.GroupOfIssue;
using WebBff.Api.Services.Issues.Issues;
using WebBff.Api.Services.Issues.Statuses;
using WebBff.Api.Services.Issues.StatusFlow;
using WebBff.Api.Services.Issues.TypeOfGroupOfIssue;
using WebBff.Api.Services.Issues.TypeOfIssue;

namespace WebBff.Api
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
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebBff.Api", Version = "v1" });
            });

            services.AddScoped<IGroupOfIssueService, GrpcGroupOfIssueService>();
            services.AddScoped<IIssueService, GrpcIssueService>();
            services.AddScoped<IStatusService, GrpcStatusService>();
            services.AddScoped<IStatusFlowService, GrpcStatusFlowService>();
            services.AddScoped<ITypeOfGroupOfIssueService, GrpcTypeOfGroupOfIssueService>();
            services.AddScoped<ITypeOfIssueService, GrpcTypeOfIssueService>();

            AddIssueServiceClient(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebBff.Api v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddIssueServiceClient(IServiceCollection services)
        {

            services.AddGrpcClient<GroupOfIssueService.GroupOfIssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
            services.AddGrpcClient<IssueService.IssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
            services.AddGrpcClient<StatusService.StatusServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
            services.AddGrpcClient<StatusFlowService.StatusFlowServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
            services.AddGrpcClient<TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
            services.AddGrpcClient<TypeOfIssueService.TypeOfIssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            });
        }
    }
}
