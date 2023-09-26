using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Contract;
using Domain.TaskAggregation;

namespace TaskFeature
{
    internal class UserChangesTheStatusOfATask
    {
        private readonly ITaskService _service;
        private ChangeTheTaskStatus? _request = null;
        private Func<System.Threading.Tasks.Task>? _actual = null;

        internal UserChangesTheStatusOfATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToChangeTheStatusOfATask(Guid taskId, TaskStatus newStatus)
        {
            _request = new ChangeTheTaskStatus(taskId, newStatus);
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
