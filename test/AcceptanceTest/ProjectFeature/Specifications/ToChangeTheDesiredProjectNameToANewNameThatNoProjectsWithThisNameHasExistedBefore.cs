using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;
using System.Xml.Linq;

namespace AcceptanceTest.TaskModule
{
    internal class ToChangeTheDesiredProjectNameToANewNameThatNoProjectsWithThisNameHasExistedBefore
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal ToChangeTheDesiredProjectNameToANewNameThatNoProjectsWithThisNameHasExistedBefore(IServiceScope scope)
        {
            _service = scope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal async Task GivenIWantToChangeADesiredProjectToANewName(string nameOfTheDesiredProject, string newName)
        {
            var idOfTheDesiredProject =
                await _service.Process(new DefineANewProject(nameOfTheDesiredProject));

            _request = new ChangeTheProjectName(idOfTheDesiredProject, newName);
        }
        internal void AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted()
        {
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDone()
        {
            await _actual.Should().NotThrowAsync();
        }
    }
}
