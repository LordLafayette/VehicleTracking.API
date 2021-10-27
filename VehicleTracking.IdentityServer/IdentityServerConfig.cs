using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace VehicleTracking.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "tracking-client",
                    AllowOfflineAccess=true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret=false,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:3111/auth/sign-in-callback" },
                    AllowedScopes = {
                        "tracking-api"
                        ,IdentityServerConstants.StandardScopes.OpenId
                        ,IdentityServerConstants.StandardScopes.Profile
                        ,IdentityServerConstants.StandardScopes.Email
                        ,IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
                new List<ApiScope>
                {
                    new ApiScope("tracking-api")
                    {
                        UserClaims = { "preferred_username", "role" }
                    },
                    new ApiScope("tracking-client"),
                    new ApiScope("tracking-admin")
                };

        public static IEnumerable<IdentityResource> IdentityResources =>
                new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResource("role",new string[] {"role"})
                };
    }
}
