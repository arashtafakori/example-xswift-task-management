using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Contract;
using Domain.TaskAggregation;
using System.Threading.Tasks;

namespace TaskFeature
{
    internal class UserChangesTheStatusOfATask
    {
        private readonly ITaskService _service;
        private ChangeTheTaskStatus? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheStatusOfATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToChangeTheStatusOfATask(Guid taskId, Domain.TaskAggregation.TaskStatus newStatus)
        {
            _request = new ChangeTheTaskStatus(taskId, newStatus);
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
