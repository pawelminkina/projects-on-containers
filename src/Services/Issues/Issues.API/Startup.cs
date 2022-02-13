using Issues.API.GrpcServices;
using Issues.Domain.Issues;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Architecture.DDD.Repositories;
using EventBus;
using EventBus.Abstraction;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.PersistentConnection;
using FluentValidation;
using Issues.API.Infrastructure.Database.Seeding;
using Issues.API.Infrastructure.Grpc.Interceptors;
using Issues.API.Infrastructure.Validation;
using Issues.Application.Common.Services.Files;
using Issues.Application.CQRS.Issues.Commands.CreateIssue;
using Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.CreateType;
using Issues.Application.IntegrationEvents.EventHandlers;
using Issues.Application.IntegrationEvents.Events;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure;
using Issues.Infrastructure.Database;
using Issues.Infrastructure.Processing;
using Issues.Infrastructure.Repositories;
using Issues.Infrastructure.Services.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
                options.Interceptors.Add<GrpcErrorInterceptor>();
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

            //Validators
            //behaviour which will log every cqs request would be nice
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(CreateTypeOfGroupOfIssuesCommandValidator).Assembly);

            services.AddScoped<ICsvFileReader, CsvFileReader>();
            services.AddScoped<IIssueSeedItemService, IssueCsvSeedItemService>();

            AddCustomConfiguration(services);
            ConfigureAuthService(services);

            //Event handlers
            services.AddScoped<OrganizationCreatedIntegrationEventHandler>();
            services.AddScoped<OrganizationDeletedIntegrationEventHandler>();

            AddEventBus(services);
        }

        private void AddCustomConfiguration(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<IssueServiceSeedingOptions>(Configuration);

        }

        protected virtual void AddEventBus(IServiceCollection services)
        {
            //Eventbus
            services.Configure<RabbitMQOptions>(Configuration.GetSection("RabbitMQOptions"));
            services.AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
        }

        protected void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrganizationCreatedIntegrationEvent, OrganizationCreatedIntegrationEventHandler>();
            eventBus.Subscribe<OrganizationDeletedIntegrationEvent, OrganizationDeletedIntegrationEventHandler>();
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

        protected virtual void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = Configuration.GetValue<string>("AuthServiceHttpExternalUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidAudiences = new System.Collections.Generic.List<string>()
                    {
                        "issues_api",
                        "internal_communication_scope"
                    }
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            ConfigureAuth(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapGrpcService<GrpcIssueService>();
                endpoints.MapGrpcService<GrpcGroupOfIssueService>();
                endpoints.MapGrpcService<GrpcStatusFlowService>();
                endpoints.MapGrpcService<GrpcTypeOfGroupOfIssueService>();
            });

            ConfigureEventBus(app);

        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

        }
    }
}
