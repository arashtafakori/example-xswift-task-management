using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to change the status of a task
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToChangeTheStatusOfATaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToChangeTheStatusOfATaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }
 
        [Fact]
        internal async Task ToChangeTheStatusOfATask()
        {
            var steps = new ToChangeTheStatusOfATask(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope, 
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            var newStatus = Module.Domain.TaskAggregation.TaskStatus.InProgress;

            steps.Given(_ => steps.GivenIWantToChangeTheStatusOfATask(taskId, newStatus))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
