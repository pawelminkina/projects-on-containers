using Auth.API.Auth.Configuration;
using Auth.API.Auth.GrantTypes;
using Auth.API.Auth.Profiles;
using Auth.API.Infrastructure.Auth;
using Auth.API.Infrastructure.Grpc.Interceptors;
using Auth.Core.Models;
using Auth.Core.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Users.API.Protos;

namespace Auth.API
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
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();

            //Options
            services.Configure<AuthenticationOptions>(Configuration);

            //Identity Server
            services.AddIdentityServer(config =>
            {
                config.UserInteraction.LoginUrl = "/Auth/Login";
                config.UserInteraction.LogoutUrl = "/Auth/Logout";
                config.UserInteraction.ErrorUrl = "/Auth/error";
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryClients(IdentityServerConfiguration.GetClients(GetClientUrls()))
            .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
            .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
            .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
            .AddExtensionGrantValidator<InternalCommunicationGrantValidator>();

            services.AddScoped<IProfileService, POCProfileService>();

            //Services
            services.AddScoped<ILoginService<POCUser>, GrpcLoginService>();
            services.AddScoped<IUserService<POCUser>, GrpcLoginService>();
            services.AddTransient<IInternalJwtTokenFactory, InternalJwtTokenFactory>();


            //Grpc
            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddTransient<JwtTokenForwardingInterceptor>();
            services.AddGrpcClient<UserService.UserServiceClient>((services, options) =>
                {
                    options.Address = new Uri(Configuration["UserServiceGrpcUrl"]);
                })
                .AddInterceptor<JwtTokenForwardingInterceptor>()
                .AddInterceptor<GrpcExceptionInterceptor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(b =>
            {
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        protected virtual Dictionary<string, string> GetClientUrls()
        {
            var clientUrls = new Dictionary<string, string>();
            clientUrls.Add("WebBFFAggregator", Configuration.GetValue<string>("WebBffAggregatorHttpExternalUrl"));
            return clientUrls;

        }
    }
}
