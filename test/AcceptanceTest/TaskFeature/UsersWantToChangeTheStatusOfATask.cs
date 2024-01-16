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
    /// I want to change the status of a task
    /// So that I should be able to access the task with the new status
    /// </summary>
    public class UsersWantToChangeTheStatusOfATask : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public UsersWantToChangeTheStatusOfATask(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }
 
        [Fact]
        internal async Task GivenUserChangesTheStatusOfATask_WhenChangingTheStatus_ThenShouldChangeItSuccessfully()
        {
            ITaskService _service = _serviceScope.ServiceProvider.GetRequiredService<ITaskService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope, 
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            var newStatus = Module.Domain.TaskAggregation.TaskStatus.InProgress;

            // Given
            var request = new ChangeTheTaskStatus(taskId, newStatus);

            // When
            Func<Task> actual = async () => await _service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear Down
            _fixture.EnsureRecreatedDatabase(); 
        }
    }
}
