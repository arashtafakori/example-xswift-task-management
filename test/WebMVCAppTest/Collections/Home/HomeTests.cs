//using Microsoft.Extensions.DependencyInjection;
//using System.Net.Http;
//using System.Security.Policy;
//using System.Threading.Tasks;
//using XSwift.Mvc;
//using Xunit;

//namespace WebApiTest
//{
//    public class HomeTests : IClassFixture<ProjectFixture>
//    {
//        private IServiceScope _serviceScope;
//        private readonly ProjectFixture _fixture;
//        private readonly HttpClient _httpClient;
//        public HomeTests(ProjectFixture fixture)
//        {
//            _fixture = fixture;
//            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();

//            var httpClientFactory = _serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
//            _httpClient = httpClientFactory.CreateClient(HttpClientNames.WebAPIClient);
//        }

//        [Theory]
//        [InlineData("d")]
//        [InlineData("Index")]
//        [InlineData("About")]
//        [InlineData("Privacy")]
//        [InlineData("Contact")]
//        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string actionName)
//        {
//            // Arrange

//            // Act
//            var response = await _httpClient.GetAsync("/home/" + actionName);

//            // Assert
//            response.EnsureSuccessStatusCode(); // Status Code 200-299
//            Assert.Equal("text/html; charset=utf-8",
//                response.Content.Headers.ContentType.ToString());
//        }
//    }
//}
