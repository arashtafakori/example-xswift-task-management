using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using XSwift.Mvc;
using System.Net;
using Module.Domain.SprintAggregation;

namespace WebApiTest
{
    public class SprintsAPIsTests : IClassFixture<SprintsFixture>
    {
        private HttpService _projectHttpService;
        private HttpService _sprintHttpService;
        public SprintsAPIsTests(SprintsFixture fixture)
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

            _sprintHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Sprints);
        }

        [Fact]
        public async Task GetInfoList_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _sprintHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get,
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Sprints));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetInfoList_V1_1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _sprintHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, version: "v1.1",
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Sprints));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetInfo_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            // Act
            var response = await _sprintHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, collectionItemParameter: sprintId));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Define_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            string sprintName = WordHelper.GetARandomName();

            // Act
            var response = await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Post, new DefineASprint(projectId, sprintName)));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task ChangeTheSprintName_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            var newName = WordHelper.GetARandomName();

            // Act
            var response = await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch, 
                new ChangeTheSprintName(sprintId, newName),
                actionName: "ChangeTheSprintName"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task CheckTheItemForArchiving_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            // Act
            var response = await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Get,
                actionName: HttpServiceBasicActionsName.CheckTheItemForArchiving,
                collectionItemParameter: sprintId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Archive_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            // Act
            var response = await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: sprintId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Restore_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: sprintId));

            // Act
            var response = await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Restore,
                collectionItemParameter: sprintId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }
    }
}
