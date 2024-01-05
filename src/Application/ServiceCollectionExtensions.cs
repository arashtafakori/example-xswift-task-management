using Microsoft.Extensions.DependencyInjection;
using XSwift.Settings;
using SoftDeleteServices.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Module.Contract;
using MediatR;
using Module.Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XSwift.Datastore;
using XSwift.EntityFrameworkCore;

namespace Module.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSetting databaseSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null,
            SqlServerSetting? sqlServerSetting = null)
        {
            services.ConfigureDataStore(
                databaseSetting,
                inMemoryDatabaseSetting,
                sqlServerSetting);

            // MediatR Registrations
            services.AddMediatR(typeof(ProjectService));
            services.AddMediatR(typeof(Persistence.ProjectRepository.DefineAProjectHandler));
            services.AddMediatR(typeof(Domain.ProjectAggregation.DefineAProject));

            // Application Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<ITaskService, TaskService>();

            // Infrastructure Services
        }

        private static void ConfigureDataStore(
            this IServiceCollection services,
            DatabaseSetting databaseSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null,
            SqlServerSetting? sqlServerSetting = null)
        {
            if (databaseSetting.IsInMemory)
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseInMemoryDatabase(databaseName: inMemoryDatabaseSetting!.DatabaseName)
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
                       sqlServerSetting!.ConnectString!,
                       b => b.MigrationsAssembly(assembly)),
                   ServiceLifetime.Scoped);

                services.RegisterSoftDelServicesAndYourConfigurations(
                    Assembly.GetAssembly(typeof(ModuleDeletabilityConfiguration)));
            }

            new DatabaseInitialization(services, databaseSetting).Initialize();
        }
    }
 }
