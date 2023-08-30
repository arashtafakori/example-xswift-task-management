using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CoreX.TestingFacilities;
using Contract;
using Domain.ProjectAggregation;
using CoreX.Domain;

namespace SprintFeature
{
    internal class UserChangesTheNameOfASprintToANewNameWhichHasAlreadyBeenReservedForAnotherSprint
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfASprintToANewNameWhichHasAlreadyBeenReservedForAnotherSprint(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToChangeTheNameOfASprintToANewName(Guid sprintId, string newSprintName)
        {
            _request = new ChangeTheProjectName(sprintId, newSprintName);
        }
        internal async Task AndGivenASprintWithThisNameHasAlreadyBeenExisted(string newName)
        {
            await _service.Process(new DefineANewProject(newName));
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<AnEntityWithThisSpecificationHasAlreadyBeenExisted>();
        }
    }
}
