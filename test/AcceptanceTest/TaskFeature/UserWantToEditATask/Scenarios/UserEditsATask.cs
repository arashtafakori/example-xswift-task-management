using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Contract;
using Domain.TaskAggregation;

namespace TaskFeature
{
    internal class UserEditsATask
    {
        private readonly ITaskService _service;
        private EditTheTask? _request = null;
        private Func<Task>? _actual = null;

        internal UserEditsATask(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToEditATask(Guid taskId, string newDescription,
            TaskStatus newStatus, Guid? newSprintId)
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
