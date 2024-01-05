using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Module.Contract;
using Module.Domain.TaskAggregation;
using System.Threading.Tasks;

namespace AcceptanceTest.TaskFeature
{
    internal class ToRestoreAnArchivedTask
    {
        private readonly ITaskService _service;
        private RestoreTheTask? _request = null;
        private Func<Task>? _actual = null;

        internal ToRestoreAnArchivedTask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToRestoreAnArchivedTask(Guid taskId)
        {
            _request = new RestoreTheTask(taskId);
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
