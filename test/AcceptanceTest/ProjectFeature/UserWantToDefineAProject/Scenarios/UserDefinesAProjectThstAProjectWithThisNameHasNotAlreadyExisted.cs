using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CoreX.TestingFacilities;
using Contract;
using Domain.ProjectAggregation;
using CoreX.Domain;

namespace ProjectFeature
{
    internal class UserDefinesAProjectThstAProjectWithThisNameHasNotAlreadyExisted
    {
        private readonly IProjectService _service;
        private DefineAProject? _request = null;
        private Func<Task>? _actual = null;

        internal UserDefinesAProjectThstAProjectWithThisNameHasNotAlreadyExisted(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToDefineAProject(string name)
        {
            _request = new DefineAProject(name);
        }
        internal async Task AndGivenAProjectWithThisNameHasAlreadyBeenExisted(string name)
        {
            await _service.Process(new DefineAProject(name));
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<AnEntityWithTheseConditionsOfExistenceHasAlreadyBeenExisted>();
        }
    }
}
