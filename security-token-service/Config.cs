using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerHost
{
	public class Config
	{
        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResource(
                    "roles",
                    "",
                    new List<string>() { "role" })
          };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] {
                new ApiScope(ApplicationScopes.Development),
                new ApiScope(ApplicationScopes.ProjectSettings),
                new ApiScope(ApplicationScopes.Board)
            };
        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {          
          };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                   {
                        ClientId = "TaskManagement.WebAPI",
                        ClientName = "TaskManagement WebAPI",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = {
                        "roles",
                        ApplicationScopes.ProjectSettings,
                        ApplicationScopes.Board }
                    },
                new Client
                   {
                        ClientId = "TaskManagement.WebAPI.Dev",
                        ClientName = "TaskManagement WebAPI Dev",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = {
                        "roles",
                        ApplicationScopes.Development,
                        ApplicationScopes.ProjectSettings,
                        ApplicationScopes.Board }
                    },
                new Client
                   {
                        ClientId = "TaskManagement.WebMVCApp.Dev",
                        ClientName = "TaskManagement WebMVCApp Dev",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = {
                        "roles",
                        ApplicationScopes.Development,
                        ApplicationScopes.ProjectSettings,
                        ApplicationScopes.Board }
                    },
                new Client
                   {
                       ClientId = "TaskManagement.WebMVCApp",
                       ClientName = "TaskManagement WebMVCApp",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RequireConsent = true,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:7011/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:7011/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Email,
                           "roles",
                           ApplicationScopes.Development,
                           ApplicationScopes.ProjectSettings,
                           ApplicationScopes.Board
                       }
                   }
            };
    }
}
