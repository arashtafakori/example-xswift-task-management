using AcceptanceTest;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to restore a project
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToRestoreAProjectSoThatICanDoTheRequest : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToRestoreAProjectSoThatICanDoTheRequest(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToRestoreAnArchivedProject()
        {
            var steps = new ToRestoreAnArchivedProject(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new ArchiveTheProject(projectId));

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedProject(projectId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
