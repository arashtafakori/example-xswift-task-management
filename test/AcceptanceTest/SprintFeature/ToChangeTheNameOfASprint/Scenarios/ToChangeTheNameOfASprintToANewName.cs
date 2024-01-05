using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Module.Contract;
using Module.Domain.SprintAggregation;

namespace AcceptanceTest.SprintFeature
{
    internal class ToChangeTheNameOfASprintToANewName
    {
        private readonly ISprintService _service;
        private ChangeTheSprintName? _request = null;
        private Func<Task>? _actual = null;

        internal ToChangeTheNameOfASprintToANewName(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToChangeTheNameOfASprintToANewName(Guid sprintId, string newSprintName)
        {
            _request = new ChangeTheSprintName(sprintId, newSprintName);
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
