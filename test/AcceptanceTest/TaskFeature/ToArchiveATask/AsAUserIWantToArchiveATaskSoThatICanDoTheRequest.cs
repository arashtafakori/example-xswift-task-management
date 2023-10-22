using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to archive a task
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToArchiveATaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToArchiveATaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToArchiveATask()
        {
            var steps = new ToArchiveATask(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var taskId = await dataFacilitator.AddATask(
                projectId,
                description: "Define a new module as the task module.",
                null);

            steps.Given(_ => steps.GivenIWantToArchiveATask(taskId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
