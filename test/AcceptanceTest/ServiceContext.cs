using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Persistence;
using Module.Presentation.Configuration;
using System;
using XSwift.MassTransit;
using XSwift.Settings;

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

            var databaseSetting = new DatabaseSetting(configuration);
            var inMemoryDatabaseSetting = new InMemoryDatabaseSetting(configuration);
            inMemoryDatabaseSetting.SetInMemoryDatabaseName(Guid.NewGuid().ToString());

            services.ConfigureApplicationServices(
                databaseSetting: databaseSetting,
                inMemoryDatabaseSetting: inMemoryDatabaseSetting,
                massTransitSetting: new MassTransitSetting(configuration));

            services.ConfigureLanguage(configuration.GetSection("AppLanguage").Value!);

            ServiceProvider = services.BuildServiceProvider(validateScopes: true);

            EnsureRecreatedDatabase();
        }

        public void EnsureRecreatedDatabase()
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