using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Issues.Tests.Core.Auth
{
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

            context.User.AddIdentity(identity);

            await _next.Invoke(context);
        }
    }
}
