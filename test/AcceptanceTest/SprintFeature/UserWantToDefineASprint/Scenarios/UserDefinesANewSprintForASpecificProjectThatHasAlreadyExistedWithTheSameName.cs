using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CoreX.TestingFacilities;
using Contract;
using Domain.SprintAggregation;
using CoreX.Domain;

namespace SprintFeature
{
    internal class UserDefinesANewSprintForASpecificProjectThatHasAlreadyExistedWithTheSameName
    {
        private readonly ISprintService _service;
        private DefineANewSprint? _request = null;
        private Func<Task>? _actual = null;

        internal UserDefinesANewSprintForASpecificProjectThatHasAlreadyExistedWithTheSameName(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToDefineANewSprintForASpecificProject(Guid projectId, string sprintName)
        {
            _request = new DefineANewSprint(projectId, sprintName);
        }
        internal async Task AndGivenASprintWithThisNameHasAlreadyBeenExistedForThisProject(
            Guid projectId, string sprintName)
        {
            await _service.Process(new DefineANewSprint(projectId, sprintName));
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
