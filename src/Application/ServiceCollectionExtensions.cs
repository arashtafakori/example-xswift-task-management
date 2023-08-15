using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using CoreX.Settings;
using SoftDeleteServices.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CoreX.Datastore;
using Contract;
using MediatR;
using Persistence.EFCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EntityFrameworkCore.CoreX.Datastore;
using System;

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
            services.AddMediatR(typeof(Persistence.EFCore.ProjectRepository.DefineANewProjectHandler));
            services.AddMediatR(typeof(Domain.ProjectAggregation.DefineANewProject));

            // Application Services
            services.AddScoped<IProjectService, ProjectService>();

            // Domain Services

            // Infrastructure Services
        }

        private static void ConfigureDataStore(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            if (databaseSettings.UsingBy == DatabaseType.InMemory)
            {
                services.AddScoped<IDatabase, ModuleEFCoreDatabase>();
                services.AddScoped<IDbTransaction, ModuleDbTransaction>();

                services.AddDbContext<ModuleDbContext>(options =>
                   options.UseInMemoryDatabase("mydb")
                   .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                   ServiceLifetime.Scoped);
            }
            else if (databaseSettings.UsingBy == DatabaseType.SqlServer)
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
            else if (databaseSettings.UsingBy == DatabaseType.MongoDb)
            {
            }
        }

        public static void AddRequestClients(
            this IBusRegistrationConfigurator busServiceConfigurator)
        {
        }
    }
 }
