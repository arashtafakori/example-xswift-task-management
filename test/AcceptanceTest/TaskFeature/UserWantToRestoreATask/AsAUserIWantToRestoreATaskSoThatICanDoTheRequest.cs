using AcceptanceTest;
using Contract;
using Domain.TaskAggregation;
using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to restore a task
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToRestoreATaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToRestoreATaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserRestoresADeletedTask()
        {
            var steps = new UserRestoresADeletedTask(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var taskId = await dataFacilitator.AddATask(
               projectId,
               description: "Define a new module as the task module.",
               null);

            await _serviceScope.ServiceProvider.
                GetRequiredService<ITaskService>().Process(
                new ArchiveTheTask(taskId));

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedTask(taskId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
