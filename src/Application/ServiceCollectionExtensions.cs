using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using XSwift.Settings;
using SoftDeleteServices.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using XSwift.Datastore;
using Contract;
using MediatR;
using Persistence.EFCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings)
        {
            services.ConfigureDataStore(databaseSettings);

            // MediatR Registrations
            services.AddMediatR(typeof(ProjectService));
            services.AddMediatR(typeof(Persistence.EFCore.ProjectRepository.DefineAProjectHandler));
            services.AddMediatR(typeof(Domain.ProjectAggregation.DefineAProject));

            // Application Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<ITaskService, TaskService>();

            // Infrastructure Services
        }

        private static void ConfigureDataStore(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            if (databaseSettings.Type == DatabaseType.InMemory)
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseInMemoryDatabase(databaseName: databaseSettings.InMemoryDatabaseName)
                   .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                   ServiceLifetime.Scoped);
            }
            else if (databaseSettings.Type == DatabaseType.SqlServer)
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                var assembly = typeof(ModuleDbContext).Assembly.GetName().Name;
                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseSqlServer(
                       databaseSettings.SqlServerConnectString!,
                       b => b.MigrationsAssembly(assembly)),
                   ServiceLifetime.Scoped);

                services.RegisterSoftDelServicesAndYourConfigurations(
                    Assembly.GetAssembly(typeof(ModuleDeletabilityConfiguration)));
            }
            else if (databaseSettings.Type == DatabaseType.MongoDb)
            {
            }
        }

        public static void AddRequestClients(
            this IBusRegistrationConfigurator busServiceConfigurator)
        {
        }
    }
 }
