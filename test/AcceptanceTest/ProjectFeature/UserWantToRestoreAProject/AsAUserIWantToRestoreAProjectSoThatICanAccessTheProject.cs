using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskModule
{
    /// <summary>
    /// As a user
    /// I want to restore a project
    /// So that I can access the project
    /// </summary>
    public class AsAUserIWantToRestoreAProjectSoThatICanAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope? _scope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToRestoreAProjectSoThatICanAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void UserRestoresADeletedProject()
        {
            var steps = new UserRestoresADeletedProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToRestoreADeletedProjectWithTheName(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
