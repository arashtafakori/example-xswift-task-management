using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using FluentAssertions.XSwift;
using Contract;
using Domain.ProjectAggregation;
using XSwift.Domain;

namespace ProjectFeature
{
    internal class ToDefineAProjectAndGivenAProjectWithThisNameHasAlreadyExisted
    {
        private readonly IProjectService _service;
        private DefineAProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToDefineAProjectAndGivenAProjectWithThisNameHasAlreadyExisted(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToDefineAProject(string name)
        {
            _request = new DefineAProject(name);
        }
        internal async Task AndGivenAProjectWithThisNameHasAlreadyExisted(string name)
        {
            await _service.Process(new DefineAProject(name));
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted>();
        }
    }
}
