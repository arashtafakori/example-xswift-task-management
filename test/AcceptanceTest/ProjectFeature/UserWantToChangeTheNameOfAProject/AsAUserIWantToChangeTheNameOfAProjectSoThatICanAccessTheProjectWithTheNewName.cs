using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskModule
{
    /// <summary>
    /// As a user
    /// I want to change the name of a project
    /// So that I can access the project with the new name
    /// </summary>
    public class AsAUserIWantToChangeTheNameOfAProjectSoThatICanAccessTheProjectWithTheNewName : IClassFixture<ProjectFixture>
    {
        private IServiceScope? _scope;
        private readonly ProjectFixture _fixture;
        public AsAUserIWantToChangeTheNameOfAProjectSoThatICanAccessTheProjectWithTheNewName(ProjectFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void UserChangesTheProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject()
        {
            var steps = new UserChangesTheDesiredProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject(_scope!);
            var nameOfTheDesiredProject = "mars";
            var newName = "earth";

            steps.Given(_ => steps.GivenIWantToChangeADesiredProjectToANewName(nameOfTheDesiredProject, newName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyBeenExisted(newName))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void UserChangesTheProjectNameToANewNameThatNoProjectsWithThisNameHasAlreadyExisted()
        {
            var steps = new UserChangesTheDesiredProjectNameToANewNameThatNoProjectsWithThisNameHasExistedBefore(_scope!);
            var nameOfTheDesiredProject = "mars";
            var newName = "earth";

            steps.Given(_ => steps.GivenIWantToChangeADesiredProjectToANewName(nameOfTheDesiredProject, newName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
