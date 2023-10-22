using AcceptanceTest;
using Contract;
using Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to define a sprint to a project
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToDefineASprintSoThatICanDoTheRequest : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToDefineASprintSoThatICanDoTheRequest(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToDefineASprintToAProject()
        {
            var steps = new ToDefineASprintToAProject(_serviceScope!);

            var projectService = _serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
            var projectName = "Task Management";
            var projectId = await projectService.Process(new DefineAProject(projectName));

            var sprintName = "Sprint 01";

            steps.Given(_ => steps.GivenIWantToDefineASprintForAProject(projectId, sprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task ToDefineASprintToAProjectAndGivenASprintWithThisNameHasAlreadyExistedForThisProject()
        {
            var steps = new ToDefineASprintToAProjectAndGivenASprintWithThisNameHasAlreadyExistedForThisProject(_serviceScope!);

            var projectId = await new DataFacilitator(_serviceScope).
                DefineAProject(projectName: "Task Managment");
            var sprintName = "Sprint 01";

            steps.Given(_ => steps.GivenIWantToDefineASprintForAProject(projectId, sprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasAlreadyExistedForThisProject(projectId, sprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
