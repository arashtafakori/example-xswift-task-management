using Microsoft.Extensions.DependencyInjection;
using Doit.AccountModule.Persistence.EFCore;
using Support.AuthModule.Messaging;
using MassTransit;
using CoreX.Settings;
using SoftDeleteServices.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CoreX.Datastore;

namespace Doit.AccountModule.Application
{
    public static class Configurator
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings)
        {
            services.ConfigureDataStore(databaseSettings);

            // MediatR Registrations
            //services.AddMediatR(typeof(Doit.AccountModule.Application.AccountService));
            //services.AddMediatR(typeof(Persistence.EFCore.AccountRepository.RegisterANewAccountHandler));
            //services.AddMediatR(typeof(Domain.AccountAggregation.RegisterANewAccount));

            // Application Services
            //services.AddScoped<IAccountService, AccountService>();

            // Domain Services

            // Infrastructure Services
        }

        private static void ConfigureDataStore(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
            services.AddScoped<IDbTransaction, ModuleDbTransaction>();

            if (databaseSettings.UsingBy == DatabaseType.InMemory)
            {
                //services.AddDbContext<ModuleDbContext>(options =>
                //   options.UseInMemoryDatabase("mydb")
                //   .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                //   ServiceLifetime.Scoped);
            }
            else if (databaseSettings.UsingBy == DatabaseType.SqlServer)
            {
                services.RegisterSoftDelServicesAndYourConfigurations(
                    Assembly.GetAssembly(typeof(ModuleDeletabilityConfiguration)));

                var assembly = typeof(ModuleDbContext).Assembly.GetName().Name;
                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseSqlServer(
                       databaseSettings.SqlServerConnectString!,
                       b => b.MigrationsAssembly(assembly)),
                   ServiceLifetime.Scoped);
            }
            else if (databaseSettings.UsingBy == DatabaseType.MongoDb)
            {
            }
        }

        public static void AddRequestClients(
            this IBusRegistrationConfigurator busServiceConfigurator)
        {
            busServiceConfigurator.AddRequestClient<RequstAVerificationCodeAsEmailAuthenticityMessage>();
            busServiceConfigurator.AddRequestClient<VerifyAndRegisterNewAUserAsEmailAuthenticityMessage>();
        }
    }
 }
