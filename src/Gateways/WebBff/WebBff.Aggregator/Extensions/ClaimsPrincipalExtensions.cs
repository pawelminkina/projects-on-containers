using System.Security.Claims;

namespace WebBff.Aggregator.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetOrganizationId(this ClaimsPrincipal claimsPrincipal)
        {
            var org = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "organizationId");
            if (org == null)
                throw new InvalidOperationException("No organization id claim has been found");
            return org.Value;
        }
    }
}
