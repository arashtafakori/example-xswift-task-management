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
    /// I want to restore a project
    /// So that I can access the project
    /// </summary>
    public class AsAUserIWantToRestoreAProjectSoThatICanAccessTheProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToRestoreAProjectSoThatICanAccessTheProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserRestoresADeletedProject()
        {
            var steps = new UserRestoresADeletedProject(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Management");

            await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new ArchiveTheProject(projectId));

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedProject(projectId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
