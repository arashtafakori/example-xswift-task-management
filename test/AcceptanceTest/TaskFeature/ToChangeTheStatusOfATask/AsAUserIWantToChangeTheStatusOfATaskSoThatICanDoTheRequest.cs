using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace TaskFeature
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

            var dataFacilitator = new ApplicationServiceFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var taskId = await dataFacilitator.AddATask(
                projectId,
                description: "Define a new module as the task module.",
                null);
            var newStatus = Domain.TaskAggregation.TaskStatus.InProgress;

            steps.Given(_ => steps.GivenIWantToChangeTheStatusOfATask(taskId, newStatus))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
