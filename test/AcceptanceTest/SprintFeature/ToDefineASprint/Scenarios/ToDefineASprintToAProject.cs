using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Module.Contract;
using Module.Domain.SprintAggregation;

namespace AcceptanceTest.SprintFeature
{
    internal class ToDefineASprintToAProject
    {
        private readonly ISprintService _service;
        private DefineASprint? _request = null;
        private Func<Task>? _actual = null;

        internal ToDefineASprintToAProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToDefineASprintForAProject(Guid projectId, string sprintName)
        {
            _request = new DefineASprint(projectId, sprintName);
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
