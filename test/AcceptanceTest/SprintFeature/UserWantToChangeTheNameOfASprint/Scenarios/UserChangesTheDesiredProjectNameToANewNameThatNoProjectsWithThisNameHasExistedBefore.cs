using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace SprintFeature
{
    internal class UserChangesTheNameOfASprintToANewNameThatNoSprintsWithThisNameHasAlreadyExisted
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfASprintToANewNameThatNoSprintsWithThisNameHasAlreadyExisted(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToChangeTheNameOfASprintToANewName(Guid sprintId, string newSprintName)
        {
            _request = new ChangeTheProjectName(sprintId, newSprintName);
        }
        internal void AndGivenASprintWithThisNameHasNotAlreadyBeenExisted()
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
