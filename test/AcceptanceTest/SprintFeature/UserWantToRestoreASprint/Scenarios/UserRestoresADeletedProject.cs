using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.SprintAggregation;

namespace SprintFeature
{
    internal class UserRestoresADeletedSprint
    {
        private readonly ISprintService _service;
        private RestoreTheSprint? _request = null;
        private Func<Task>? _actual = null;

        internal UserRestoresADeletedSprint(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToRestoreAnArchivedSprint(Guid sprintId)
        {
            _request = new RestoreTheSprint(sprintId);
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
