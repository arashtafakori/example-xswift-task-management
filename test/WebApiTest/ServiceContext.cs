using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Presentation.Configuration;

namespace WebApiTest
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
            //var serviceScope = ServiceProvider.CreateAsyncScope();
            //var httpClientFactory = serviceScope.ServiceProvider
            //    .GetRequiredService<IHttpClientFactory>();
            //var httpClient = httpClientFactory.CreateClient(
            //    HttpClientNames.WebAPIClient);

            //var httpService = new HttpService(
            //    httpClient: httpClient,
            //    version: "v1",
            //    collectionResource: "development");

            //await httpService.SendAsync(
            //    new XHttpRequest(HttpMethod.Post, actionName: "EnsureRecreatedDatabase"));
        }
        public void Dispose()
        {
            if (ServiceProvider != null)
                ServiceProvider.Dispose();
        }
    }
}