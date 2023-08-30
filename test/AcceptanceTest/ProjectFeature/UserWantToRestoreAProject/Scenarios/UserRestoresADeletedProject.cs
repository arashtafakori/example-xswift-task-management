using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace ProjectFeature
{
    internal class UserRestoresADeletedProject
    {
        private readonly IProjectService _service;
        private RestoreTheProject? _request = null;
        private Func<Task>? _actual = null;

        internal UserRestoresADeletedProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToRestoreAnArchivedProject(Guid projectId)
        {
            _request = new RestoreTheProject(projectId);
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
