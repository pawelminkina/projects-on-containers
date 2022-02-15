using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using EventBus;
using EventBus.Abstraction;
using EventBus.InMemory;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.PersistentConnection;
using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Users.Core.CQRS.Users.Commands.CreateUser;
using Users.Core.CQRS.Users.Queries.GetUserById;
using Users.Core.Infrastructure.MappingProfiles;
using Users.API.GrpcServices;
using Users.API.Infrastructure.Database;
using Users.API.Infrastructure.Grpc.Interceptors;
using Users.API.Infrastructure.Validation;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.Interceptors.Add<GrpcErrorInterceptor>();
            });

            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
            services.AddMediatR(
                typeof(UserServiceDbContext).Assembly, //DAL
                typeof(CreateUserCommandHandler).Assembly); //Core

            services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddHttpContextAccessor();

            services.AddTransient<GrpcErrorInterceptor>();
            services.AddScoped<IUserSeedItemService, UserCsvSeedItemService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(GetUserByIdQueryValidator).Assembly);
            
            services.AddHttpContextAccessor();

            ConfigureAuthService(services);

            ConfigureIdentity(services);
            AddDatabase(services);
            AddCustomConfiguration(services);   
            AddEventBus(services);

            //Health check
            AddHealthChecks(services);
        }

        protected virtual void AddHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(Configuration["ConnectionString"], name: "UserService-DB-check", tags: new string[] { "UserServiceDB" })
                .AddRabbitMQ($"amqp://{Configuration["RabbitMQOptions:HostName"]}", name: "UserService-RabbitMQ-check", tags: new string[] { "rabbitmq" });

        }

        protected virtual void AddEventBus(IServiceCollection services)
        {
            services.Configure<RabbitMQOptions>(Configuration.GetSection("RabbitMQOptions"));
            services.AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
        }
        private void AddCustomConfiguration(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<UserServiceDbSeedingOptions>(Configuration);

        }
        public void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<UserServiceDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionString"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(UserServiceDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                },
                ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            ConfigureAuth(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapGrpcService<GrpcOrganizationService>();
                endpoints.MapGrpcService<GrpcUserService>();

                MapHealthChecks(endpoints);
            });
        }

        protected virtual void MapHealthChecks(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
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
                        "user_api",
                        "internal_communication_scope"
                    }
                };
            });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            //This method is part of content from .AddIdentity method from Microsoft.AspNetCore.Identity
            //It is added this way because default implementation is adding authentication with cookie and custom login path, which is a problem with external identity service
            services.TryAddScoped<IUserValidator<UserDAO>, UserValidator<UserDAO>>();
            services.TryAddScoped<IPasswordValidator<UserDAO>, PasswordValidator<UserDAO>>();
            services.TryAddScoped<IPasswordHasher<UserDAO>, PasswordHasher<UserDAO>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<UserDAO>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<UserDAO>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<UserDAO>, UserClaimsPrincipalFactory<UserDAO, IdentityRole>>();
            services.TryAddScoped<IUserConfirmation<UserDAO>, DefaultUserConfirmation<UserDAO>>();
            services.TryAddScoped<UserManager<UserDAO>>();
            services.TryAddScoped<SignInManager<UserDAO>>();
            services.TryAddScoped<RoleManager<IdentityRole>>();

            new IdentityBuilder(typeof(UserDAO), typeof(IdentityRole), services)
                .AddEntityFrameworkStores<UserServiceDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
