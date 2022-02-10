using IdentityModel.Client;

namespace Auth.API.Infrastructure.Auth
{
    public class InternalJwtTokenFactory : IInternalJwtTokenFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<InternalJwtTokenFactory> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public InternalJwtTokenFactory(IHttpContextAccessor httpContextAccessor, ILogger<InternalJwtTokenFactory> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetInternalJwtTokenAsync()
        {
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

            var tokenRequest = new TokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "internalcommunicationclient",
                GrantType = "internal_communication",
                ClientSecret = "superSecretScope",
            };

            tokenRequest = AddTokenToRequestIfCallerIsAuthenticated(tokenRequest);

            var tokenResponse = await httpClient.RequestTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception($"Error occured while trying to get token, error: {tokenResponse.Error}", tokenResponse.Exception);
            }

            var internalJwtToken = tokenResponse.TokenType + " " + tokenResponse.AccessToken;

            _logger.LogDebug("Created internal jwt token: {internalJwtToken}", internalJwtToken);

            return internalJwtToken;
        }

        private TokenRequest AddTokenToRequestIfCallerIsAuthenticated(TokenRequest request) => string.IsNullOrWhiteSpace(_authorizationHeader)
            ? request
            : GetRequestWithUserToken(request, _authorizationHeader);

        private string GetTokenWithoutType(string token) => token.Contains(' ')
            ? token.Split(' ').ElementAtOrDefault(1)
            : token;

        private string _authorizationHeader =>
            _httpContextAccessor.HttpContext?.Request.Headers
            .FirstOrDefault(h => h.Key.ToUpper().Equals("Authorization".ToUpper())).Value.ToString();

        private TokenRequest GetRequestWithUserToken(TokenRequest request, string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                throw new Exception($"Authorization header is empty, when should contain user token");

            jwtToken = GetTokenWithoutType(jwtToken);

            request.Parameters = new Dictionary<string, string>(new KeyValuePair<string, string>[]
            {
                new("token", jwtToken)
            });

            return request;
        }
    }
}
