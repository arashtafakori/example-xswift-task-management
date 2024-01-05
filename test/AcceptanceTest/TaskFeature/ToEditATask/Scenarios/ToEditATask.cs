using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Module.Contract;
using Module.Domain.TaskAggregation;
using System.Threading.Tasks;

namespace AcceptanceTest.TaskFeature
{
    internal class ToEditATask
    {
        private readonly ITaskService _service;
        private EditTheTask? _request = null;
        private Func<Task>? _actual = null;

        internal ToEditATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToEditATask(Guid taskId, string newDescription,
            Module.Domain.TaskAggregation.TaskStatus newStatus, Guid? newSprintId)
        {
            _request = new EditTheTask(taskId, newDescription, 
                newStatus, newSprintId);
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
