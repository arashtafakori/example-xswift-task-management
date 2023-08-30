using AcceptanceTest;
using Contract;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to restore a sprint
    /// So that I can access the sprint
    /// </summary>
    public class AsAUserIWantToRestoreASprintSoThatICanAccessTheSprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToRestoreASprintSoThatICanAccessTheSprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserRestoresADeletedSprint()
        {
            var steps = new UserRestoresADeletedSprint(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Managment");
            var sprintId = await dataBuilder.DefineASprint(
                projectId, sprintName: "Sprint 01");

            await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new ArchiveTheSprint(sprintId));

            steps.Given(_ => steps.GivenIWantToRestoreAnArchivedSprint(sprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
