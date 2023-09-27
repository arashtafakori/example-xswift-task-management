using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using FluentAssertions.XSwift;
using Contract;
using Domain.SprintAggregation;
using XSwift.Domain;

namespace SprintFeature
{
    internal class UserDefinesASprintForAProjectThatASprintWithThisNameHasAlreadyExistedForThisProject
    {
        private readonly ISprintService _service;
        private DefineASprint? _request = null;
        private Func<Task>? _actual = null;

        internal UserDefinesASprintForAProjectThatASprintWithThisNameHasAlreadyExistedForThisProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToDefineASprintForAProject(Guid projectId, string sprintName)
        {
            _request = new DefineASprint(projectId, sprintName);
        }
        internal async Task AndGivenASprintWithThisNameHasAlreadyBeenExistedForThisProject(
            Guid projectId, string sprintName)
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
