using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.SprintAggregation;

namespace SprintFeature
{
    internal class UserChangesTheNameOfASprintToANewNameThatNoSprintWithThisNameHasAlreadyExistedInTheSameProject
    {
        private readonly ISprintService _service;
        private ChangeTheSprintName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfASprintToANewNameThatNoSprintWithThisNameHasAlreadyExistedInTheSameProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToChangeTheNameOfASprintToANewName(Guid sprintId, string newSprintName)
        {
            _request = new ChangeTheSprintName(sprintId, newSprintName);
        }
        internal void AndGivenASprintWithThisNameHasNotAlreadyBeenExistedInTheSameProject()
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
