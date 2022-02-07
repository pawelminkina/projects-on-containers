﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using User.Core.CQS.Users.Commands.CreateUser;
using User.Core.CQS.Users.Queries.GetUserById;
using User.Core.Infrastructure.MappingProfiles;
using Users.API.GrpcServices;
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

            services.AddControllers();
            services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddHttpContextAccessor();

            //DI
            services.AddTransient<GrpcErrorInterceptor>();

            //Validators
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(GetUserByIdQueryValidator).Assembly);

            ConfigureIdentity(services);
            AddDatabase(services);
            

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapGrpcService<GrpcOrganizationService>();
                endpoints.MapGrpcService<GrpcUserService>();
            });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            //This method is copy of content from .AddIdentity method from Microsoft.AspNetCore.Identity
            //It is added this way because default implementation is not allowing to authenticate user using JWT token
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