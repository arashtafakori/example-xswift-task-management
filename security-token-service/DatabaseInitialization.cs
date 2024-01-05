using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServerHost.Data;
using System.Security.Claims;

namespace IdentityServerHost
{
	public class DatabaseInitialization
    {
        private readonly string _connectionString;
        private readonly string _clientRedirectUri;
        public DatabaseInitialization(string connectionString, string clientRedirectUri)
        {
            _connectionString = connectionString;
            _clientRedirectUri = clientRedirectUri;
        }
        public void Initialize()
        {

            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(_connectionString)
            );

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            _connectionString,
                            sql => sql.MigrationsAssembly(typeof(DatabaseInitialization).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            _connectionString,
                            sql => sql.MigrationsAssembly(typeof(DatabaseInitialization).Assembly.FullName)
                        );
                }
            );

            //--

            var serviceProvider = services.BuildServiceProvider();

            using var serviceScope = serviceProvider.
                GetRequiredService<IServiceScopeFactory>().CreateScope();
            //
            var persistedGrantDbContext = serviceScope.ServiceProvider
                .GetService<PersistedGrantDbContext>();
            if (!persistedGrantDbContext!.Database.GetAppliedMigrations().Any())
            {
                persistedGrantDbContext!.Database.Migrate();

                var configurationDbContext = serviceScope.ServiceProvider
                    .GetService<ConfigurationDbContext>();
                configurationDbContext!.Database.Migrate();
                CreateDefaultResources(configurationDbContext);
                //
                var aspNetIdentityDbContext = serviceScope.ServiceProvider
                    .GetService<AspNetIdentityDbContext>();
                aspNetIdentityDbContext!.Database.Migrate();
                CreateDefaultUsers(serviceScope);
            }
        }

        private void CreateDefaultResources(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
        private void CreateDefaultUsers(IServiceScope serviceScope)
        {
            DefineAnAdminUser(serviceScope);
            DefineAContributerUser(serviceScope);
            //
            var dbContext = serviceScope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            dbContext!.SaveChanges();
        }

        private void DefineAnAdminUser(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager.FindByNameAsync("admin").Result;
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "email@domain.com",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "a@T123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userManager.AddClaimsAsync(
                        user,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "The Admin"),
                            new Claim(JwtClaimTypes.GivenName, ""),
                            new Claim(JwtClaimTypes.FamilyName, ""),
                            new Claim(JwtClaimTypes.WebSite, ""),
                            new Claim(JwtClaimTypes.Role, Roles.Admin)
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
        private void DefineAContributerUser(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager.FindByNameAsync("contributer").Result;
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "contributer",
                    Email = "email@domain.com",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "a@T123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userManager.AddClaimsAsync(
                        user,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "The Contributer"),
                            new Claim(JwtClaimTypes.GivenName, ""),
                            new Claim(JwtClaimTypes.FamilyName, ""),
                            new Claim(JwtClaimTypes.WebSite, ""),
                            new Claim(JwtClaimTypes.Role, Roles.Contributer)
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
