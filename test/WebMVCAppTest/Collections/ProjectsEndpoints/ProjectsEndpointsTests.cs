using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using XSwift.Mvc;
using System.Net;

namespace WebMVCAppTest
{
    public class ProjectsEndpointsTests : IClassFixture<ProjectsFixture>
    {
        private HttpClient _httpClient;
        public ProjectsEndpointsTests(ProjectsFixture fixture)
        {
            var serviceScope = fixture.ServiceProvider.CreateAsyncScope();
            var httpClientFactory = serviceScope.ServiceProvider
                .GetRequiredService<IHttpClientFactory>();
            _httpClient = httpClientFactory.CreateClient(
                HttpClientNames.WebMVCAppClient);
        }

        [Fact]
        public async Task GetInfoList_V1_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var defaultPage = await _httpClient.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var quoteElement = content.QuerySelector("#quote");

            // Assert
 
        }
    }
}
