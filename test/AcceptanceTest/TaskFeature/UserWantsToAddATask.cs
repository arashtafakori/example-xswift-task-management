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
    /// I want to add a task to a project
    /// So that I should be able to access the task
    /// </summary>
    public class UserWantsToAddATask : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public UserWantsToAddATask(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserAddsATask_WhenAddingTask_ThenShouldAddingItSuccessfully()
        {
            ITaskService _service = _serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
 
            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var description = "Add a new module as the task module.";
            Guid? sprintId = null;

            // Given
            var _request = new AddATask(projectId, description, sprintId);

            // When
            Func<Task>? actual = async () => await _service.Process(_request!);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear Down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
