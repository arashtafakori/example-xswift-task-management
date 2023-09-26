using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.TaskAggregation;

namespace TaskFeature
{
    internal class UserAddsATaskToAProject
    {
        private readonly ITaskService _service;
        private AddATask? _request = null;
        private Func<System.Threading.Tasks.Task>? _actual = null;

        internal UserAddsATaskToAProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
        }
        internal void GivenIWantToAddATaskToAProject(Guid projectId, string description, Guid? sprintId)
        {
            _request = new AddATask(projectId, description, sprintId);
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
