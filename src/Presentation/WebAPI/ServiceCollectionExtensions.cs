using Microsoft.IdentityModel.Tokens;
using XSwift.OAuth;
using Presentation.Configuration.AuthDefinitions;

namespace Presentation.WebAPI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthService(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var jwtBearerSettings = new JwtBearerSettings(configuration);

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = jwtBearerSettings.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Policies.ClientId,
                    policy => policy.RequireClaim(
                        "client_id", "TaskManagement.WebAPI", "TaskManagement.WebMVCApp"));
                options.AddPolicy(
                    Policies.ProjectsSettingsScope,
                    policy => policy.RequireClaim(
                        "scope", ApplicationScopes.ProjectSettings));
                options.AddPolicy(
                    Policies.BoardScope,
                    policy => policy.RequireClaim(
                        "scope", ApplicationScopes.Board));
            });
        }
    }
}
