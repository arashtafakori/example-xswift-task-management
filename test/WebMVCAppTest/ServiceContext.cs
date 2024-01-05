using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Configuration;

namespace WebMVCAppTest
{
    public abstract class ServiceContext
    {
        public ServiceProvider ServiceProvider { get; set; }
        public ServiceContext()
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.Test.json").Build();
 
            var services = new ServiceCollection();

            services.ConfigureLanguage(configuration.GetSection("AppLanguage").Value!);

            services.HttpClientService(configuration);

            ServiceProvider = services.BuildServiceProvider(validateScopes: true);
        }

        public async void EnsureRecreatedDatabase()
        {
        }
        public void Dispose()
        {
            if (ServiceProvider != null)
                ServiceProvider.Dispose();
        }
    }
}