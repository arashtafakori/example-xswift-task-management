using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Module.Contract;
using Module.Domain.ProjectAggregation;

namespace AcceptanceTest.ProjectFeature
{
    internal class ToDefineAProject
    {
        private readonly IProjectService _service;
        private DefineAProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToDefineAProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToDefineAProject(string name)
        {
            _request = new DefineAProject(name);
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
