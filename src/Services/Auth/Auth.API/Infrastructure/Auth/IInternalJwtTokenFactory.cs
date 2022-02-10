namespace Auth.API.Infrastructure.Auth
{
    /// <summary>
    /// Factory which allows you to access JWT token used for internal communication.
    /// Internal communication token should never be exposed for a user, because they allow to access any method from any available service.
    /// Internal token will contain current user id and other custom claims extracted from JWT Token used in current request.
    /// Thanks to that, internal communication token will behave in the same way in particular service, as the token which have direct scope for this service.
    /// </summary>
    public interface IInternalJwtTokenFactory
    {
        /// <summary>
        /// Get internal communication token with userId and custom claims, which are included in auth service internal communication token validator
        /// </summary>
        /// <returns></returns>
        Task<string> GetInternalJwtTokenAsync();
    }
}
