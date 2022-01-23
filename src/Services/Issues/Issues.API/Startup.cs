using Issues.API.GrpcServices;
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
using System.Reflection;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using FluentValidation;
using Issues.API.Infrastructure.Database.Migration;
using Issues.API.Infrastructure.Database.Seeding;
using Issues.API.Infrastructure.Validation;
using Issues.Application.Common.Services.Files;
using Issues.Application.CQRS.Issues.Commands.CreateIssue;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.CreateType;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure;
using Issues.Infrastructure.Database;
using Issues.Infrastructure.Processing;
using Issues.Infrastructure.Repositories;
using Issues.Infrastructure.Services.Files;
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
        //TODO TypeOfIssueInTypeOfGroup and GroupOfIssues also contain flow, there should be one way to assign flows, or they need to correctly synchronized!
        //TODO Add validatiors for all cqs
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);

            services.AddMediatR(
                typeof(IIssueRepository).Assembly, //Domain
                typeof(CreateIssueCommand).Assembly); //Application

            AddDatabase(services);

            services.AddScoped<IGroupOfIssuesRepository, SqlGroupOfIssuesRepository>();
            services.AddScoped<IIssueRepository, SqlIssueRepository>();
            services.AddScoped<ITypeOfGroupOfIssuesRepository, SqlGroupOfIssuesRepository>();
            services.AddScoped<IStatusFlowRepository, SqlStatusFlowRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddControllers();

            //Validators
            //behaviour which will log every cqs request would be nice
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(CreateTypeOfGroupOfIssuesCommandValidator).Assembly);

            services.AddScoped<ICsvFileReader, CsvFileReader>();
            services.AddScoped<IIssueSeedItemService, IssueCsvSeedItemService>();

            AddCustomConfiguration(services);
        }

        private void AddCustomConfiguration(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<IssueServiceSeedingOptions>(Configuration);

        }

        public void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<IssuesServiceDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionString"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(IssuesServiceDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                },
                ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );
        }

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
                endpoints.MapGrpcService<GrpcTypeOfGroupOfIssueService>();
            });
        }
    }
}
