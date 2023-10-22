using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to define a project
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToDefineAProjectSoThatICanDoTheRequest : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToDefineAProjectSoThatICanDoTheRequest(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void ToDefineAProject()
        {
            var steps = new ToDefineAProject(_serviceScope!);

            var projectName = "Task Management";

            steps.Given(_ => steps.GivenIWantToDefineAProject(projectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
        [Fact]
        internal void ToDefineAProjectAndGivenAProjectWithThisNameHasAlreadyExisted()
        {
            var steps = new ToDefineAProjectAndGivenAProjectWithThisNameHasAlreadyExisted(_serviceScope!);

            var projectName = "Task Management";

            steps.Given(_ => steps.GivenIWantToDefineAProject(projectName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyExisted(projectName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
