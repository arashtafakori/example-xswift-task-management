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
    internal class UserChangesTheNameOfAProjectToANewNameWhichHasAlreadyBeenReservedForAnotherProject
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfAProjectToANewNameWhichHasAlreadyBeenReservedForAnotherProject(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToChangeTheNameOfAProjectToANewName(Guid projectId, string newProjectName)
        {
            _request = new ChangeTheProjectName(projectId, newProjectName);
        }
        internal async Task AndGivenAProjectWithThisNameHasAlreadyBeenExisted(string projectName)
        {
            await _service.Process(new DefineAProject(projectName));
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
