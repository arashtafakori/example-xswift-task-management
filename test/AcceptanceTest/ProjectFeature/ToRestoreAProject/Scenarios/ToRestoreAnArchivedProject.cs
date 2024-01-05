using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Module.Contract;
using Module.Domain.ProjectAggregation;

namespace AcceptanceTest.ProjectFeature
{
    internal class ToRestoreAnArchivedProject
    {
        private readonly IProjectService _service;
        private RestoreTheProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToRestoreAnArchivedProject(IServiceScope serviceScope)
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
