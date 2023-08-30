using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to change the name of a project
    /// So that I can access the project with the new name
    /// </summary>
    public class AsAUserIWantToChangeTheNameOfAProjectSoThatICanAccessTheProjectWithTheNewName : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToChangeTheNameOfAProjectSoThatICanAccessTheProjectWithTheNewName(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserChangesTheNameOfAProjectToANewNameWhichHasAlreadyBeenReservedForAnotherProject()
        {
            var steps = new UserChangesTheNameOfAProjectToANewNameWhichHasAlreadyBeenReservedForAnotherProject(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Management");

            var newProjectName = "Task Board";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfAProjectToANewName(projectId, newProjectName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyBeenExisted(newProjectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task UserChangesTheNameOfAProjectToANewNameThatNoProjectsWithThisNameHasAlreadyExisted()
        {
            var steps = new UserChangesTheNameOfAProjectToANewNameThatNoProjectsWithThisNameHasAlreadyExisted(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Management");

            var newProjectName = "Task Board";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfAProjectToANewName(projectId, newProjectName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
