using AcceptanceTest;
using Contract;
using Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to archive a project
    /// So that I can not access the project
    /// </summary>
    public class AsAUserIWantToArchiveAProjectSoThatICanNotAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToArchiveAProjectSoThatICanNotAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserArchivesAProject()
        {
            var steps = new UserArchivesAProject(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Management");

            steps.Given(_ => steps.GivenIWantToArchiveAProject(projectId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
