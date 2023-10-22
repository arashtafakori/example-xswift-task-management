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
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToArchiveASprintSoThatICanDoTheRequest : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToArchiveASprintSoThatICanDoTheRequest(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task ToArchiveASprint()
        {
            var steps = new ToArchiveASprint(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var sprintId = await dataFacilitator.DefineASprint(
                projectId, sprintName: "Sprint 01");

            steps.Given(_ => steps.GivenIWantToArchiveASprint(sprintId))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }


    }
}
