using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Auth.API.Auth.Profiles
{
    public class POCProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
