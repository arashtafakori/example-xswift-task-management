using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Module.Contract;
using Module.Domain.SprintAggregation;

namespace AcceptanceTest.SprintFeature
{
    internal class UserRestoresAnArchivedSprint
    {
        private readonly ISprintService _service;
        private RestoreTheSprint? _request = null;
        private Func<Task>? _actual = null;

        internal UserRestoresAnArchivedSprint(IServiceScope serviceScope)
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
