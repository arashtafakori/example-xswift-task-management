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
    /// So that I can access do the request
    /// </summary>
    public class AsAUserIWantToChangeTheNameOfAProjectSoThatICanDoTheRequest : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToChangeTheNameOfAProjectSoThatICanDoTheRequest(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToChangeTheNameOfAProjectToANewName()
        {
            var steps = new ToChangeTheNameOfAProjectToANewName(_serviceScope!);

            var projectId = await new DataFacilitator(_serviceScope)
                .DefineAProject(projectName: "Task Management");

            var newProjectName = "Task Board";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfAProjectToANewName(projectId, newProjectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task ToChangeTheNameOfAProjectToANewAndGivenAProjectWithTheSameNewNameHasAlreadyExisted()
        {
            var steps = new ToChangeTheNameOfAProjectToANewAndGivenAProjectWithTheSameNewNameHasAlreadyExisted(_serviceScope!);

            var projectId = await new DataFacilitator(_serviceScope)
                .DefineAProject(projectName: "Task Management");

            var newProjectName = "Task Board";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfAProjectToANewName(projectId, newProjectName))
                .Given(_ => steps.AndGivenAProjectWithTheSameNewNameHasAlreadyExisted(newProjectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
