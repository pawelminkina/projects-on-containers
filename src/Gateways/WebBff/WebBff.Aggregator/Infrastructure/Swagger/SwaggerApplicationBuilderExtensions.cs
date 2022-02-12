using Microsoft.Extensions.Options;
using WebBff.Aggregator.Infrastructure.Swagger.Options;

namespace WebBff.Aggregator.Infrastructure.Swagger
{
    public static class SwaggerApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithAuthorization(this IApplicationBuilder app)
        {
            var swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;

            app.UseSwagger().UseSwaggerUI(uiOptions =>
            {
                uiOptions.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);

                if (swaggerOptions.OAuth != null)
                {
                    uiOptions.OAuthClientId(swaggerOptions.OAuth.ClientId);
                    uiOptions.OAuthClientSecret(swaggerOptions.OAuth.ClientSecret);
                    uiOptions.OAuthRealm(swaggerOptions.OAuth.ClientRealm);
                    uiOptions.OAuthAppName(swaggerOptions.OAuth.ClientName);
                }
            });

            return app;
        }
    }
}
