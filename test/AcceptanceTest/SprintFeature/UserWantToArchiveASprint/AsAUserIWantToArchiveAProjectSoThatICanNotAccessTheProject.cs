using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to archive a sprint
    /// So that I can not access the sprint
    /// </summary>
    public class AsAUserIWantToArchiveASprintSoThatICanNotAccessTheSprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToArchiveASprintSoThatICanNotAccessTheSprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserArchivesASprint()
        {
            var steps = new UserArchivesASprint(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "TaskManagment");
            var sprintId = await dataBuilder.DefineASprint(
                projectId, sprintName: "Sprint 01");

            steps.Given(_ => steps.GivenIWantToArchiveASprint(sprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }


    }
}
