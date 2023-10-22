using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace ProjectFeature
{
    internal class ToArchiveAProject
    {
        private readonly IProjectService _service;
        private ArchiveTheProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToArchiveAProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToArchiveAProject(Guid projectId)
        {
            _request = new ArchiveTheProject(projectId);
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
