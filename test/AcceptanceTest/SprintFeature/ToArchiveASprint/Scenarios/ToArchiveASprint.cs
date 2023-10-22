using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.SprintAggregation;

namespace SprintFeature
{
    internal class ToArchiveASprint
    {
        private readonly ISprintService _service;
        private ArchiveTheSprint? _request = null;
        private Func<Task>? _actual = null;

        internal ToArchiveASprint(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToArchiveASprint(Guid sprintId)
        {
            _request = new ArchiveTheSprint(sprintId);
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
