using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using XSwift.Settings;
using SoftDeleteServices.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Contract;
using MediatR;
using Persistence.EFCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EntityFrameworkCore.XSwift;
using XSwift.Datastore;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null,
            SqlServerSettings? sqlServerSettings = null)
        {
            services.ConfigureDataStore(
                databaseSettings,
                inMemoryDatabaseSettings,
                sqlServerSettings);

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

        private static void ConfigureDataStore(
            this IServiceCollection services,
            DatabaseSettings databaseSettings,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null,
            SqlServerSettings? sqlServerSettings = null)
        {
            if (databaseSettings.IsInMemory)
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseInMemoryDatabase(databaseName: inMemoryDatabaseSettings.DatabaseName)
                   .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                   ServiceLifetime.Scoped);
            }
            else
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                var assembly = typeof(ModuleDbContext).Assembly.GetName().Name;
                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseSqlServer(
                       sqlServerSettings.ConnectString!,
                       b => b.MigrationsAssembly(assembly)),
                   ServiceLifetime.Scoped);

                services.RegisterSoftDelServicesAndYourConfigurations(
                    Assembly.GetAssembly(typeof(ModuleDeletabilityConfiguration)));
            }
        }

        public static void AddRequestClients(
            this IBusRegistrationConfigurator busServiceConfigurator)
        {
        }
    }
 }
