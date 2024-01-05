using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskFeature
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
        internal async Task ToEditATask()
        {
            var steps = new ToEditATask(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var taskId = await DataFacilitator.AddATask(
                _serviceScope,
                projectId,
                description: "Define a new module as the task module.",
                sprintId: null);

            var newDescription = "Implement the project feature as an application service.";

            Guid? newSprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, "Sprint 01"); ;

            var newStatus = Module.Domain.TaskAggregation.TaskStatus.Completed;

            steps.Given(_ => steps.GivenIWantToEditATask(taskId, newDescription,
                newStatus, newSprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
