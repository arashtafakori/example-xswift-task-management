using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CoreX.TestingFacilities;
using Contract;
using Domain.ProjectAggregation;
using CoreX.Domain;

namespace AcceptanceTest.TaskModule
{
    internal class UserChangesTheDesiredProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheDesiredProjectNameToANewNameWhichHasAlreadyBeenReservedForAnotherProject(IServiceScope scope)
        {
            _service = scope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal async Task GivenIWantToChangeADesiredProjectToANewName(string nameOfTheDesiredProject, string newName)
        {
            var idOfTheDesiredProject =
                await _service.Process(new DefineANewProject(nameOfTheDesiredProject));

            _request = new ChangeTheProjectName(idOfTheDesiredProject, newName);
        }
        internal async Task AndGivenAProjectWithThisNameHasAlreadyBeenExisted(string newName)
        {
            await _service.Process(new DefineANewProject(newName));
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<TheEntityWithThisSpecificationHasAlreadyBeenExisted>();
        }
    }
}
