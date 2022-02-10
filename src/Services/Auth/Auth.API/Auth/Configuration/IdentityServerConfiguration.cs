using IdentityServer4;
using IdentityServer4.Models;

namespace Auth.API.Auth.Configuration
{
    public class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "internalcommunicationclient",
                    ClientName = "Internal Communication Client",
                    AllowedGrantTypes = { "internal_communication" },
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("superSecretScope".Sha256())},
                    AllowedScopes =
                    {
                        "internal_communication_scope"
                    }
                },

            };
        }

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {            
            new ApiResource("internal_communication_scope", "Internal Communication") { Scopes = new string[] { "internal_communication_scope" } },
            new ApiResource("user_api", "User API") { Scopes = new string[] { "user_api" } },
            new ApiResource("issues_api", "Issues API") { Scopes = new string[] { "issues_api" } },
        };
    }
}
