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
	public class DataSeeder
	{
		public static void Seed(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(connectionString)
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
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(DataSeeder).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(DataSeeder).Assembly.FullName)
                        );
                }
            );

            //--

            var serviceProvider = services.BuildServiceProvider();

            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>()!.Database.Migrate();
            //
            var configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
            configurationDbContext!.Database.Migrate();
            SeedDefaultResources(configurationDbContext);
            //
            var aspNetIdentityDbContext = serviceScope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            aspNetIdentityDbContext!.Database.Migrate();
            SeedUsers(serviceScope);
        }

        private static void SeedDefaultResources(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
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
        private static void SeedUsers(IServiceScope serviceScope)
        {
            DefineAnAdminUser(serviceScope);
            DefineAContributerUser(serviceScope);
            //
            var dbContext = serviceScope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            dbContext!.SaveChanges();
        }
        private static void DefineAnAdminUser(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager.FindByNameAsync("arash").Result;
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "arash",
                    Email = "arash.software.dev@email.com",
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
                            new Claim(JwtClaimTypes.Name, "Arash Tafakori"),
                            new Claim(JwtClaimTypes.GivenName, "Arash"),
                            new Claim(JwtClaimTypes.FamilyName, "Tafakori"),
                            new Claim(JwtClaimTypes.WebSite, "http://artaware.ir"),
                            new Claim(JwtClaimTypes.Role, Roles.Admin)
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
        private static void DefineAContributerUser(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager.FindByNameAsync("arta").Result;
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "arta",
                    Email = "arashcoolfire@email.com",
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
                            new Claim(JwtClaimTypes.Name, "Arta Tafakori"),
                            new Claim(JwtClaimTypes.GivenName, "Arta"),
                            new Claim(JwtClaimTypes.FamilyName, "Tafakori"),
                            new Claim(JwtClaimTypes.WebSite, "http://artaware.ir"),
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
