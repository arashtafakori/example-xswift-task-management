using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EFCore;
using Presentation.Configuration;

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
            services.ConfigureAndAddServices(configuration);
            ServiceProvider = services.BuildServiceProvider(validateScopes: true);

            ResetDbContext();
        }

        public void ResetDbContext()
        {
            var scope = ServiceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<ModuleDbContext>();
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