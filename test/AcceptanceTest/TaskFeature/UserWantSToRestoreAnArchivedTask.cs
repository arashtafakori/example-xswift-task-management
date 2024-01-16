using Module.Contract;
using Module.Domain.TaskAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System;
using FluentAssertions;

namespace AcceptanceTest.TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to restore an archived task
    /// So that I should be able to access the task again
    /// </summary>
    public class UserWantSToRestoreAnArchivedTask : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public UserWantSToRestoreAnArchivedTask(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserRestoresAnArchivedTask_WhenRestoringTask_TheShouldRestoreItSuccessfully()
        {
            ITaskService _service = _serviceScope.ServiceProvider.GetRequiredService<ITaskService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope,
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            await _serviceScope.ServiceProvider.
                GetRequiredService<ITaskService>().Process(
                new ArchiveTheTask(taskId));

            // Given
            var request = new RestoreTheTask(taskId);

            // When
            Func<Task> actual = async () => await _service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear Down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
