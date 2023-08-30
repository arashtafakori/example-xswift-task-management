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
    /// So that I can access the sprint with the new name
    /// </summary>
    public class AsAUserIWantToChangeTheNameOfASprintSoThatICanAccessTheSprintWithTheNewName : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToChangeTheNameOfASprintSoThatICanAccessTheSprintWithTheNewName(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task UserChangesTheNameOfASprintToANewNameWhichHasAlreadyBeenReservedForAnotherSprint()
        {
            var steps = new UserChangesTheNameOfASprintToANewNameWhichHasAlreadyBeenReservedForAnotherSprint(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Managment");
            await dataBuilder.DefineASprint(
                projectId, sprintName: "Sprint 01");
            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(projectId, newSprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasAlreadyBeenExisted(newSprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal async Task UserChangesTheNameOfASprintToANewNameThatNoSprintsWithThisNameHasAlreadyExisted()
        {
            var steps = new UserChangesTheNameOfASprintToANewNameThatNoSprintsWithThisNameHasAlreadyExisted(_serviceScope!);

            var dataBuilder = new DataFacilitator(_serviceScope);
            var projectId = await dataBuilder.DefineAProject(
                projectName: "Task Managment");
            await dataBuilder.DefineASprint(
                projectId, sprintName: "Sprint 01");

            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(projectId, newSprintName))
                .Given(_ => steps.AndGivenASprintWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
