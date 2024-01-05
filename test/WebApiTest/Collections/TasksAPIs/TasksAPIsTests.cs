using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using XSwift.Mvc;
using System.Net;
using Module.Domain.TaskAggregation;

namespace WebApiTest
{
    public class TasksAPIsTests : IClassFixture<TasksFixture>
    {
        private HttpService _projectHttpService;
        private HttpService _sprintHttpService;
        private HttpService _taskHttpService;
 
        public TasksAPIsTests(TasksFixture fixture)
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

            _taskHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Tasks);
        }

        [Fact]
        public async Task GetInfoList_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            // Act
            var response = await _taskHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get,
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Tasks));

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
            var response = await _taskHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, version: "v1.1",
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Tasks));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetInfo_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var taskId = await DataFacilitator.AddATask(
                _taskHttpService, projectId, WordHelper.GetARandomSentence());

            // Act
            var response = await _taskHttpService
                .SendAsync(new XHttpRequest(HttpMethod.Get, collectionItemParameter: taskId));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Define_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            string description = WordHelper.GetARandomSentence();

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Post, new AddATask(projectId, description)));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Edit_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var sprintId = await DataFacilitator.DefineASprint(
                _sprintHttpService, projectId, WordHelper.GetARandomName());

            var taskId = await DataFacilitator.AddATask(
                _taskHttpService, projectId, WordHelper.GetARandomSentence());

            var newDescription = WordHelper.GetARandomSentence();

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Put, 
                new EditTheTask(
                    taskId, newDescription,
                    Module.Domain.TaskAggregation.TaskStatus.InProgress, sprintId)));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task ChangeTheTaskStatus_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var taskId = await DataFacilitator.AddATask(
                _taskHttpService, projectId, WordHelper.GetARandomSentence());

            var newStatus = Module.Domain.TaskAggregation.TaskStatus.Blocked;

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                new ChangeTheTaskStatus(taskId, newStatus),
                actionName: "ChangeTheTaskStatus"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Archive_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var taskId = await DataFacilitator.AddATask(
                _taskHttpService, projectId, WordHelper.GetARandomSentence());

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: taskId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Restore_V1_ReturnsSuccessStatusCode()
        {
            // Arrange
            var projectId = await DataFacilitator.DefineAProject(
                _projectHttpService, WordHelper.GetARandomName());

            var taskId = await DataFacilitator.AddATask(
                _taskHttpService, projectId, WordHelper.GetARandomSentence());

            await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: taskId));

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Restore,
                collectionItemParameter: taskId));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.HttpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task GetTaskStatusItems_V1_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var response = await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Get, actionName: "GetTaskStatusItems"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpResponseMessage.StatusCode);
        }
    }
}
