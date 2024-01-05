using Microsoft.IdentityModel.Tokens;
using XSwift.OAuth;
using Module.Presentation.Configuration.AuthDefinitions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Module.Presentation.WebAPI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthService(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var jwtBearerSettings = new JwtBearerSetting(configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = jwtBearerSettings.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Policies.ClientsConstraint,
                    policy => policy.RequireClaim(
                        "client_id",
                        "TaskManagement.WebAPI",
                        "TaskManagement.WebAPI.Dev",
                        "TaskManagement.WebMVCApp"));
                options.AddPolicy(
                    Policies.ToAccessToTheDevelopmentFeatures,
                    policy => policy.RequireClaim(
                        "scope", ApplicationScopes.Development));
                options.AddPolicy(
                    Policies.ToAccessToTheSettings,
                    policy => policy.RequireClaim(
                        "scope", ApplicationScopes.ProjectSettings));
                options.AddPolicy(
                    Policies.ToAccessToTheBoradActitvitis,
                    policy => policy.RequireClaim(
                        "scope", ApplicationScopes.Board));
            });
        }
    }
}
