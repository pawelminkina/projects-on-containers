using System.Security.Claims;
using Auth.Core.Models;
using Auth.Core.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Auth.API.Auth.Profiles
{
    public class POCProfileService : IProfileService
    {
        private readonly IUserService<POCUser> _userService;

        public POCProfileService(IUserService<POCUser> userService)
        {
            _userService = userService;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userService.GetUserByIdAsync(sub);

            context.IssuedClaims = new List<Claim>()
            {
                new Claim("organizationId", user.OrganizationId),
            };
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userService.GetUserByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
