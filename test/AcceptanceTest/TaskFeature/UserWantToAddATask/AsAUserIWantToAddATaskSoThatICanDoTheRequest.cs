using Contract;
using Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace TaskFeature
{
    /// <summary>
    /// As a user
    /// I want to add a task to a project
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToAddATaskSoThatICanDoTheRequest : IClassFixture<TaskFixture>
    {
        private IServiceScope _serviceScope;
        private readonly TaskFixture _fixture;
        public AsAUserIWantToAddATaskSoThatICanDoTheRequest(TaskFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserAddsATaskToAProject()
        {
            var steps = new UserAddsATaskToAProject(_serviceScope!);

            var projectService = _serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
            var projectName = "Task Management";
            var projectId = await projectService.Process(
                new DefineAProject(projectName));
            var description = "Add a new module as the task module.";
            Guid? sprintId = null;

            steps.Given(_ => steps.GivenIWantToAddATaskToAProject(
                projectId, description, sprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
