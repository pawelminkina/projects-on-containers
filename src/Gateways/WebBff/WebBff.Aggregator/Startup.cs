using System.IdentityModel.Tokens.Jwt;
using Issues.API.Protos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Users.API.Protos;
using WebBff.Aggregator.Infrastructure.Auth;
using WebBff.Aggregator.Infrastructure.Grpc.ExceptionMapping;
using WebBff.Aggregator.Infrastructure.Grpc.Interceptors;
using WebBff.Aggregator.Infrastructure.Swagger;
using WebBff.Aggregator.Services.GroupOfIssues;
using WebBff.Aggregator.Services.Issues;
using WebBff.Aggregator.Services.Organization;
using WebBff.Aggregator.Services.StatusFlow;
using WebBff.Aggregator.Services.TypeOfGroupOfIssues;
using WebBff.Aggregator.Services.User;

namespace WebBff.Aggregator
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
            services.AddControllers();
            services.AddSwagger(Configuration);


            //Services
            services.AddScoped<IGroupOfIssuesService, GrpcGroupOfIssuesService>();
            services.AddScoped<IIssuesService, GrpcIssuesService>();
            services.AddScoped<IOrganizationService, GrpcOrganizationService>();
            services.AddScoped<IStatusFlowService, GrpcStatusFlowService>();
            services.AddScoped<ITypeOfGroupOfIssuesService, GrpcTypeOfGroupOfIssuesService>();
            services.AddScoped<IUsersService, GrpcUsersService>();

            //Auth
            ConfigureAuthService(services);

            //Factories
            services.AddTransient<IInternalJwtTokenFactory, InternalJwtTokenFactory>();

            //Grpc
            services.AddTransient<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<GroupOfIssueService.GroupOfIssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<IssueService.IssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<OrganizationService.OrganizationServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["UserServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<StatusFlowService.StatusFlowServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["IssueServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();

            services.AddGrpcClient<UserService.UserServiceClient>((services, options) =>
            {
                options.Address = new Uri(Configuration["UserServiceGrpcUrl"]);
            }).AddInterceptor<JwtTokenForwardingInterceptor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGrpcToHttpExceptionMapping();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseSwaggerWithAuthorization();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            });
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = Configuration.GetValue<string>("AuthServiceHttpExternalUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "web_bff_aggregator";
                options.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
        }
    }
}
