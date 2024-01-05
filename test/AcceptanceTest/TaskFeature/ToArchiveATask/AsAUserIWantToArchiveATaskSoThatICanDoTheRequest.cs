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

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope, 
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            steps.Given(_ => steps.GivenIWantToArchiveATask(taskId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
