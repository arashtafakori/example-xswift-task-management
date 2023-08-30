using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to define a project
    /// So that I can access the project
    /// </summary>
    public class AsAUserIWantToDefineAProjectSoThatICanAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToDefineAProjectSoThatICanAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void UserDefinesANewProjectThatHasAlreadyExistedWithTheSameName()
        {
            var steps = new UserDefinesANewProjectThatHasAlreadyExistedWithTheSameName(_serviceScope!);

            var projectName = "Task Management";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(projectName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyBeenExisted(projectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void UserDefinesANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject()
        {
            var steps = new UserDefinesANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject(_serviceScope!);

            var projectName = "Task Management";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(projectName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
