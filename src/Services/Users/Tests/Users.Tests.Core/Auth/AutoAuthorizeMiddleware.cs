using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Users.Tests.Core.Auth;

public class AutoAuthorizeMiddleware
{
    public const string ORGANIZATION_ID = "BaseOrganizationId";
    public const string USER_ID = "BaseUserId";

    private readonly RequestDelegate _next;

    public AutoAuthorizeMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }

    public async Task Invoke(HttpContext context)
    {
        var identity = new ClaimsIdentity("cookies");

        identity.AddClaim(new Claim("organizationId", ORGANIZATION_ID));
        identity.AddClaim(new Claim("sub", USER_ID));
        var claimsPrincipal = new ClaimsPrincipal(identity);
        context.User = claimsPrincipal;

        await _next.Invoke(context);
    }
}