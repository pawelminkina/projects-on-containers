using Microsoft.OpenApi.Models;

namespace WebBff.Aggregator.Infrastructure.Swagger.Options
{
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }

        public string Description { get; set; }

        public string UIEndpoint { get; set; }

        public OpenApiInfo ApiInfo { get; set; }

        public SwaggerOAuthOptions OAuth { get; set; }

        public string XmlCommentsFilePath { get; set; }

        public static SwaggerOptions ReadFromIConfiguration(IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection(nameof(SwaggerOptions));

            if (!swaggerSection.Exists())
                throw new ArgumentException($"Unable to read {nameof(SwaggerOptions)} configuration section");

            var swaggerOptions = new SwaggerOptions();
            swaggerSection.Bind(swaggerOptions);

            return swaggerOptions;
        }
    }
}
