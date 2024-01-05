using AcceptanceTest;
using Module.Contract;
using Module.Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to restore an archived sprint
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToRestoreATaskSoThatICanDoTheRequest : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToRestoreATaskSoThatICanDoTheRequest(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserRestoresAnArchivedSprint()
        {
            var steps = new UserRestoresAnArchivedSprint(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new ArchiveTheSprint(sprintId));

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedSprint(sprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
