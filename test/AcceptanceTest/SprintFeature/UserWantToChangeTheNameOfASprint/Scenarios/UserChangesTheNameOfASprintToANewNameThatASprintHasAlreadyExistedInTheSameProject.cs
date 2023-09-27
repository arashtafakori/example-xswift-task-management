using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using FluentAssertions.XSwift;
using Contract;
using XSwift.Domain;
using Domain.SprintAggregation;

namespace SprintFeature
{
    internal class UserChangesTheNameOfASprintToANewNameThatASprintHasAlreadyExistedInTheSameProject
    {
        private readonly ISprintService _service;
        private ChangeTheSprintName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfASprintToANewNameThatASprintHasAlreadyExistedInTheSameProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToChangeTheNameOfASprintToANewName(Guid sprintId, string newSprintName)
        {
            _request = new ChangeTheSprintName(sprintId, newSprintName);
        }
        internal async Task AndGivenASprintWithThisNameHasAlreadyExistedInTheSameProject(Guid projectId, string sprintName)
        {
            await _service.Process(new DefineASprint(projectId, sprintName));
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<AnEntityWithTheseConditionsOfExistenceHasAlreadyBeenExisted>();
        }
    }
}
