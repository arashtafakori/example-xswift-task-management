using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to edit a task
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToEditATaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToEditATaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserEditsATask()
        {
            var steps = new UserEditsATask(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            Guid? sprintId = null;

            var taskId = await dataFacilitator.AddATask(
                projectId,
                description: "Define a new module as the task module.",
                sprintId);
            var newDescription = "Implement the project feature as an application service.";

            Guid? newSprintId = await new DataFacilitator(_serviceScope).
                DefineASprint(projectId, "Sprint 01");

            var newStatus = Domain.TaskAggregation.TaskStatus.Completed;

            steps.Given(_ => steps.GivenIWantToEditATask(taskId, newDescription,
                newStatus, newSprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
