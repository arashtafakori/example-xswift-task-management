using Microsoft.IdentityModel.Tokens;
using XSwift.OAuth;
using Module.Presentation.Configuration.AuthDefinitions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

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

        public static void AddSwaggerService(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            if (configuration
                .GetSection("OAuthSetting")
                .GetSection("RunningWithIdentity").Value == "false")
            {
                services.AddSwaggerGen();
            }
            else
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });

                    // Add the Bearer token support
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                });
            }
        }

        public static void UseSwaggerService(
            this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
            });
        }
    }
}
