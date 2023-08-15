using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace AcceptanceTest.TaskModule
{
    internal class ToDefineANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject
    {
        private readonly IProjectService _service;
        private DefineANewProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToDefineANewProjectWithANameWhichHasNotAlreadyReservedForAnotherProject(IServiceScope scope)
        {
            _service = scope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToDefineANewProject(string name)
        {
            _request = new DefineANewProject(name);
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
