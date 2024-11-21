using CWB.Identity.IdentityUtils;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace CWB.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("cwb", "cwb server"),
                new ApiScope(name:"read", displayName: "read data"),
                new ApiScope(name:"write", displayName: "write data"),
                new ApiScope(name:"delete", displayName: "delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "cwbapi",
                    ClientSecrets = { new Secret("cwbsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "cwb" }
                },
                
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "cwbmvc",
                    ClientSecrets = { new Secret("cwbsecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = true,
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AlwaysIncludeUserClaimsInIdToken = true,                   
                    
                    // where to redirect to after login
                    RedirectUris = { ConfigurationHelper.config.GetSection("ApiUrls:App").Value+ "/signin-oidc" },
                    FrontChannelLogoutUri = ConfigurationHelper.config.GetSection("ApiUrls:App").Value+ "/signout-oidc" ,
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { ConfigurationHelper.config.GetSection("ApiUrls:App").Value + "/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        "cwb",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone

                    }
                }
            };
    }
}
