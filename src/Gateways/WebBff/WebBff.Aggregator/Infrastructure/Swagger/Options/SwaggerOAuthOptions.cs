namespace WebBff.Aggregator.Infrastructure.Swagger.Options
{
    public class SwaggerOAuthOptions
    {
        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ClientRealm { get; set; }

        public string ClientName { get; set; }

        public Dictionary<string, string> Scopes { get; set; }
    }
}
