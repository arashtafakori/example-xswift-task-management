using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.TaskAggregation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTest.TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to edit a task
    /// So that I should be able to access the task with the new information
    /// </summary>
    public class UserWantsToEditATask : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public UserWantsToEditATask(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserEditsATask_WhenEditityTask_ThenShouldEditItSuccessfully()
        {
            ITaskService _service = _serviceScope.ServiceProvider.GetRequiredService<ITaskService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope,
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            var newDescription = "Implement the project feature as an application service.";

            Guid? newSprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, "Sprint 01"); ;

            var newStatus = Module.Domain.TaskAggregation.TaskStatus.Completed;

            // Given
            var request = new EditTheTask(taskId, newDescription, newStatus, newSprintId);

            // When
            Func<Task> actual = async () => await _service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear Down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
