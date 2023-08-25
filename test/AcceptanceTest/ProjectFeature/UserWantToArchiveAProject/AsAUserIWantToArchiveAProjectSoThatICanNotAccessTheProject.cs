using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskModule
{
    /// <summary>
    /// As a user
    /// I want to archive a project
    /// So that I can not access the project
    /// </summary>
    public class AsAUserIWantToArchiveAProjectSoThatICanNotAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope? _scope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToArchiveAProjectSoThatICanNotAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void UserArchivesAProject()
        {
            var steps = new UserArchivesAProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToArchiveADesiredProjectWithTheName(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
