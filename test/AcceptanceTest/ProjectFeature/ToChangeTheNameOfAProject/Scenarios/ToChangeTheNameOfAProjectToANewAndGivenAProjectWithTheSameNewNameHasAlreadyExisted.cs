using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XSwift.FluentAssertions;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using XSwift.Domain;

namespace AcceptanceTest.ProjectFeature
{
    internal class ToChangeTheNameOfAProjectToANewAndGivenAProjectWithTheSameNewNameHasAlreadyExisted
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal ToChangeTheNameOfAProjectToANewAndGivenAProjectWithTheSameNewNameHasAlreadyExisted(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToChangeTheNameOfAProjectToANewName(Guid projectId, string newProjectName)
        {
            _request = new ChangeTheProjectName(projectId, newProjectName);
        }
        internal async Task AndGivenAProjectWithTheSameNewNameHasAlreadyExisted(string projectName)
        {
            await _service.Process(new DefineAProject(projectName));
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
