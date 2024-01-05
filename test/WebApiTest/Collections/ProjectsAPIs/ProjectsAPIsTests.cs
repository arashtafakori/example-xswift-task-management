using Module.Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using XSwift.Mvc;
using System.Net;

namespace WebApiTest
{
    public class ProjectsAPIsTests : IClassFixture<ProjectsFixture>
    {
        private HttpService _projectHttpService;
        public ProjectsAPIsTests(ProjectsFixture fixture)
        {
            var serviceScope = fixture.ServiceProvider.CreateAsyncScope();
            var httpClientFactory = serviceScope.ServiceProvider
                .GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(
                HttpClientNames.WebAPIClient);

            _projectHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Projects);
        }

        [Fact]
        public async Task GetInfoList_V1_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var response = await _projectHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetInfoList_V1_1_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var response = await _projectHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, version: "v1.1"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetInfo_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _projectHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, collectionItemParameter: projectId));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Define_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectName = WordHelper.GetARandomName();

            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Post, new DefineAProject(projectName)));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task ChangeTheProjectName_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var newName = WordHelper.GetARandomName();

            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch, 
                new ChangeTheProjectName(projectId, newName),
                actionName: "ChangeTheProjectName"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task CheckTheItemForArchiving_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Get,
                actionName: HttpServiceBasicActionsName.CheckTheItemForArchiving,
                collectionItemParameter: projectId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Archive_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: projectId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Restore_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: projectId));

            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Restore,
                collectionItemParameter: projectId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Delete_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());
             
            // Act
            var response = await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Delete,
                collectionItemParameter: projectId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }
    }
}
