using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Contract;
using Domain.TaskAggregation;

namespace TaskFeature
{
    internal class UserArchivesATask
    {
        private readonly ITaskService _service;
        private ArchiveTheTask? _request = null;
        private Func<System.Threading.Tasks.Task>? _actual = null;

        internal UserArchivesATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToArchiveATask(Guid taskId)
        {
            _request = new ArchiveTheTask(taskId);
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async System.Threading.Tasks.Task ThenTheRequestSholudBeDone()
        {
            await _actual.Should().NotThrowAsync();
        }
    }
}
