namespace Auth.API.Auth.Configuration
{
    public class AuthenticationOptions
    {
        public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromMinutes(15);

        public bool AllowRefresh { get; set; } = true;
    }
}
