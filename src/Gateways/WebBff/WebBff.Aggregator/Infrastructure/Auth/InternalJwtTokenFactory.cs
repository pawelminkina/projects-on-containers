using IdentityModel.Client;

namespace WebBff.Aggregator.Infrastructure.Auth
{
    public class InternalJwtTokenFactory : IInternalJwtTokenFactory
    {
        private readonly ILogger<InternalJwtTokenFactory> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public InternalJwtTokenFactory(ILogger<InternalJwtTokenFactory> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetInternalJwtTokenAsync(string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                throw new ArgumentNullException(nameof(jwtToken));

            var httpClient = _httpClientFactory.CreateClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = _configuration.GetValue<string>("AuthServiceHttpExternalUrl"),
                Policy = { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw new Exception($"Error occured while trying to read discovery document, error: {disco.Error}", disco.Exception);
            }

            var tokenResponse = await httpClient.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "internalcommunicationclient",
                GrantType = "internal_communication",
                ClientSecret = "superSecretScope",
                Parameters = new Dictionary<string, string>()
                {
                    {"token",jwtToken}
                },
            });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Error occured while trying to get token, error: {tokenResponse.Error}", tokenResponse.Exception);
            }

            var internalJwtToken = tokenResponse.TokenType + " " + tokenResponse.AccessToken;

            _logger.LogDebug("Created internal jwt token: {internalJwtToken}", internalJwtToken);

            return internalJwtToken;
        }
    }
}
