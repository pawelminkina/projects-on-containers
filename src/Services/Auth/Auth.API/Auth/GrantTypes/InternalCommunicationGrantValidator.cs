using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Auth.API.Auth.GrantTypes
{
    /// <summary>
    /// Grant validator for grant type "internal_communication".
    /// This grant type is used for communicating between services and should never be exposed for end user.
    /// Token with this grant should only be requested by gateways or micro services, but never by client.
    /// </summary>
    public class InternalCommunicationGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;
        private readonly ILogger<InternalCommunicationGrantValidator> _logger;

        public InternalCommunicationGrantValidator(ITokenValidator validator, ILogger<InternalCommunicationGrantValidator> logger)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public string GrantType => "internal_communication";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userToken = context.Request.Raw.Get("token");

            if (string.IsNullOrWhiteSpace(userToken))
            {
                _logger.LogDebug("Grant {GrantType} has been validated successfully without passing user token", GrantType);
                context.Result = new GrantValidationResult(new Dictionary<string, object>());
                return;
            }

            var result = await _validator.ValidateAccessTokenAsync(userToken);
            if (result.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Token validation failed");
                return;
            }

            var userId = result.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            context.Result = new GrantValidationResult(userId, GrantType, GetRequiredClaims(result.Claims, GetCustomUserClaims));

            _logger.LogDebug("Grant {GrantType} has been validated successfully with passing user token: {userToken}", GrantType, userToken);
        }

        private IEnumerable<Claim> GetRequiredClaims(IEnumerable<Claim> collectionToSearch, IEnumerable<string> claimsToFind)
            => collectionToSearch.Where(s => claimsToFind.Contains(s.Type));

        /// <summary>
        /// If any claims except sub claim, should be passed from end user token to internal communication token, they should be contained in this list
        /// </summary>
        private IEnumerable<string> GetCustomUserClaims => new List<string>()
        { "organizationId" };
    }
}
