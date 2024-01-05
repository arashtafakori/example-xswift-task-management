using AcceptanceTest;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to change the name of a sprint of a project
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
        internal async Task ToChangeTheNameOfASprintToANewName()
        {
            var steps = new ToChangeTheNameOfASprintToANewName(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(sprintId, newSprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
        [Fact]
        internal async Task ToChangeTheNameOfASprintToANewNameAndGivenASprintWithTheSameNewNameHasAlreadyExistedForThisProject()
        {
            var steps = new ToChangeTheNameOfASprintToANewNameAndGivenASprintWithTheSameNewNameHasAlreadyExistedForThisProject(_serviceScope!);

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            var newSprintName = "Sprint 02";

            steps.Given(_ => steps.GivenIWantToChangeTheNameOfASprintToANewName(sprintId, newSprintName))
                .Given(_ => steps.AndGivenASprintTheSameNewNameHasAlreadyExistedForThisProject(projectId, newSprintName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.EnsureRecreatedDatabase())
                .BDDfy();
        }
    }
}
