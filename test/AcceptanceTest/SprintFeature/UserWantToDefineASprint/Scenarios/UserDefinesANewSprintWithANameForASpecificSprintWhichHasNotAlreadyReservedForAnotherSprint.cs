using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.SprintAggregation;

namespace SprintFeature
{
    internal class UserDefinesANewSprintWithANameForASpecificSprintWhichHasNotAlreadyReservedForAnotherSprint
    {
        private readonly ISprintService _service;
        private DefineANewSprint? _request = null;
        private Func<Task>? _actual = null;

        internal UserDefinesANewSprintWithANameForASpecificSprintWhichHasNotAlreadyReservedForAnotherSprint(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToDefineANewSprintForASpecificProject(Guid projectId, string sprintName)
        {
            _request = new DefineANewSprint(projectId, sprintName);
        }
        internal void AndGivenASprintWithThisNameHasNotAlreadyBeenExistedForThisProject()
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
