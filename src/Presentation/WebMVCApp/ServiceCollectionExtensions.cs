using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using XSwift.OAuth;
using Module.Presentation.Configuration.AuthDefinitions;
using Microsoft.Net.Http.Headers;

namespace Module.Presentation.WebMVCApp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthService(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var openIdConnectSettings = new OpenIdConnectSetting(configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = openIdConnectSettings.Authority;

                    options.ClientId = openIdConnectSettings.ClientId;
                    options.ClientSecret = openIdConnectSettings.ClientSecret;
                    options.ResponseType = "code id_token";

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    //options.Scope.Add("email");
                    options.Scope.Add("roles");

                    options.ClaimActions.DeleteClaim("sid");
                    options.ClaimActions.DeleteClaim("idp");
                    options.ClaimActions.DeleteClaim("s_hash");
                    options.ClaimActions.DeleteClaim("auth_time");
                    options.ClaimActions.MapUniqueJsonKey("role", "role");

                    options.Scope.Add(ApplicationScopes.Development);
                    options.Scope.Add(ApplicationScopes.ProjectSettings);
                    options.Scope.Add(ApplicationScopes.Board);

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.GivenName,
                        RoleClaimType = JwtClaimTypes.Role
                    };
                });
        }

        public static void HttpClientService(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.AddTransient<AuthenticationDelegatingHandler>();

            //--

            services.AddHttpClient(HttpClientNames.WebAPIClient, client =>
            {
                var uri = configuration.GetSection("HttpClientsUri")
                .GetSection(HttpClientNames.WebAPIClient).Value!;

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            //--

            services.AddHttpContextAccessor();
        }
    }
}
