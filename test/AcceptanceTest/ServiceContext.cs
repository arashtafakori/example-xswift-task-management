using CoreX.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EFCore;
using Presentation.Configuration;
using System;

namespace AcceptanceTest
{
    public abstract class ServiceContext
    {
        public ServiceProvider ServiceProvider { get; set; }
        public ServiceContext()
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.Test.json").Build();
 
            var services = new ServiceCollection();

            var databaseSettings = new DatabaseSettings(configuration);
            databaseSettings.SetInMemoryDatabaseName(Guid.NewGuid().ToString());

            services.ConfigureAndAddServices(
                appLanguage: configuration.GetSection("AppLanguage").Value!,
                databaseSettings: databaseSettings,
                massTransitSettings: new MassTransitSettings(configuration));

            ServiceProvider = services.BuildServiceProvider(validateScopes: true);

            ResetDbContext();
        }

        public void ResetDbContext()
        {
            var serviceScope = ServiceProvider.CreateAsyncScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ModuleDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            if (ServiceProvider != null)
                ServiceProvider.Dispose();
        }
    }
}