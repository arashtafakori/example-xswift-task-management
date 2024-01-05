using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Module.Contract;
using Module.Domain.TaskAggregation;
using System.Threading.Tasks;

namespace AcceptanceTest.TaskFeature
{
    internal class ToChangeTheStatusOfATask
    {
        private readonly ITaskService _service;
        private ChangeTheTaskStatus? _request = null;
        private Func<Task>? _actual = null;

        internal ToChangeTheStatusOfATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToChangeTheStatusOfATask(Guid taskId, Module.Domain.TaskAggregation.TaskStatus newStatus)
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
