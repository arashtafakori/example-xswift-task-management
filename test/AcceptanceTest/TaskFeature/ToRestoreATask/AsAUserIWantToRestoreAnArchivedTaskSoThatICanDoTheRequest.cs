using AcceptanceTest;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using Module.Domain.TaskAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to restore an archived task
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToRestoreAnArchivedTaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToRestoreAnArchivedTaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToRestoreAnArchivedTask()
        {
            var steps = new ToRestoreAnArchivedTask(_serviceScope!);

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

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedTask(taskId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
