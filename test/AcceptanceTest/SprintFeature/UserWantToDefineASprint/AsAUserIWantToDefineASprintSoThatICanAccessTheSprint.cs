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
    /// I want to define a sprint for a specific project
    /// So that I can access the sprint
    /// </summary>
    public class AsAUserIWantToDefineASprintSoThatICanAccessTheSprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToDefineASprintSoThatICanAccessTheSprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserDefinesANewSprintForASpecificProjectThatHasAlreadyExistedWithTheSameName()
        {
            var steps = new UserDefinesANewSprintForASpecificProjectThatHasAlreadyExistedWithTheSameName(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Managment");
            var sprintName = "Sprint 01";

            steps.Given(_ => steps.GivenIWantToDefineANewSprintForASpecificProject(projectId, sprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasAlreadyBeenExistedForThisProject(projectId, sprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task UserDefinesANewSprintWithANameForASpecificSprintWhichHasNotAlreadyReservedForAnotherSprint()
        {
            var steps = new UserDefinesANewSprintWithANameForASpecificSprintWhichHasNotAlreadyReservedForAnotherSprint(_serviceScope!);

            var projectService = _serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
            var projectName = "Task Management";
            var projectId = await projectService.Process(new DefineANewProject(projectName));

            var sprintName = "Sprint 01";

            steps.Given(_ => steps.GivenIWantToDefineANewSprintForASpecificProject(projectId, sprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasNotAlreadyBeenExistedForThisProject())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
