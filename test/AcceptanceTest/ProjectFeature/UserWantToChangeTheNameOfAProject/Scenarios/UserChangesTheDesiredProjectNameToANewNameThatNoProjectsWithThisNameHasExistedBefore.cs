using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace ProjectFeature
{
    internal class UserChangesTheNameOfAProjectToANewNameThatNoProjectsWithThisNameHasAlreadyExisted
    {
        private readonly IProjectService _service;
        private ChangeTheProjectName? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheNameOfAProjectToANewNameThatNoProjectsWithThisNameHasAlreadyExisted(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal void GivenIWantToChangeTheNameOfAProjectToANewName(Guid projectId, string newProjectName)
        {
            _request = new ChangeTheProjectName(projectId, newProjectName);
        }
        internal void AndGivenAProjectWithThisNameHasNotAlreadyBeenExisted()
        {
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
