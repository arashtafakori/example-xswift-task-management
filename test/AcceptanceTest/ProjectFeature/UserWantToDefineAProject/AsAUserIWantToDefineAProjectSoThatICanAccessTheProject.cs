using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskModule
{
    /// <summary>
    /// As a user
    /// I want to define a project
    /// So that I can access the project
    /// </summary>
    public class AsAUserIWantToDefineAProjectSoThatICanAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope? _scope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToDefineAProjectSoThatICanAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void UserDefinesANewProjectThatHasAlreadyBeenDefinedWithTheSameName()
        {
            var steps = new UserDefinesANewProjectThatHasAlreadyExistedWithTheSameName(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(name))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyBeenExisted(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void UserDefinesANewProjectWithANameWhichHasNotAlreadyExisted()
        {
            var steps = new UserDefinesANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(name))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
