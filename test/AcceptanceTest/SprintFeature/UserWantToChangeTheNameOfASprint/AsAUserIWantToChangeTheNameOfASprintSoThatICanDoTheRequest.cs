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
    /// I want to change the name of a sprint that belongs to a specific project
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToChangeTheNameOfASprintSoThatICanDoTheRequest : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToChangeTheNameOfASprintSoThatICanDoTheRequest(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserChangesTheNameOfASprintToANewNameThatASprintHasAlreadyExistedInTheSameProject()
        {
            var steps = new UserChangesTheNameOfASprintToANewNameThatASprintHasAlreadyExistedInTheSameProject(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var sprintId = await dataFacilitator.DefineASprint(
                projectId, sprintName: "Sprint 01");
            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(sprintId, newSprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasAlreadyExistedInTheSameProject(projectId, newSprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task UserChangesTheNameOfASprintToANewNameThatNoSprintWithThisNameHasAlreadyExistedInTheSameProject()
        {
            var steps = new UserChangesTheNameOfASprintToANewNameThatNoSprintWithThisNameHasAlreadyExistedInTheSameProject(_serviceScope!);

            var dataFacilitator = new DataFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var sprintId = await dataFacilitator.DefineASprint(
                projectId, sprintName: "Sprint 01");
            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(sprintId, newSprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasNotAlreadyBeenExistedInTheSameProject())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
