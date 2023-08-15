using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace AcceptanceTest.TaskModule
{
    /// <summary>
    /// Feature: Project
    /// As an administrator
    /// I want to define or modify or archive a project
    /// or restor an archived project
    /// </summary>
    public class ProjectFeature : IClassFixture<Fixture>
    {
        private IServiceScope? _scope;
        private readonly Fixture _fixture;
        public ProjectFeature(Fixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal void ToDefineANewProjectThatHasAlreadyBeenDefinedWithTheSameName()
        {
            var steps = new ToDefineANewProjectThatHasAlreadyExistedWithTheSameName(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(name))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasAlreadyBeenExisted(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void ToDefineANewProjectWithANameWhichHasNotAlreadyExisted()
        {
            var steps = new ToDefineANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToDefineANewProject(name))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void ToChangeTheProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject()
        {
            var steps = new ToChangeTheDesiredProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject(_scope!);
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
        internal void ToChangeTheProjectNameToANewNameThatNoProjectsWithThisNameHasAlreadyExisted()
        {
            var steps = new ToChangeTheDesiredProjectNameToANewNameThatNoProjectsWithThisNameHasExistedBefore(_scope!);
            var nameOfTheDesiredProject = "mars";
            var newName = "earth";

            steps.Given(_ => steps.GivenIWantToChangeADesiredProjectToANewName(nameOfTheDesiredProject, newName))
                .Given(_ => steps.AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void ToArchiveADesiredProject()
        {
            var steps = new ToArchiveAProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToArchiveADesiredProjectWithTheName(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        [Fact]
        internal void ToRestoreADeletedProject()
        {
            var steps = new ToRestoreADeletedProject(_scope!);
            var name = "earth";

            steps.Given(_ => steps.GivenIWantToRestoreADeletedProjectWithTheName(name))
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDone())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }
    }
}
