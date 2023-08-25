using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace AcceptanceTest.TaskModule
{
    internal class UserRestoresADeletedProject
    {
        private readonly IProjectService _service;
        private RestoreTheProject? _request = null;
        private Func<Task>? _actual = null;

        private Guid IdOfEntityForTearinDown = Guid.Empty;

        internal UserRestoresADeletedProject(IServiceScope scope)
        {
            _service = scope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal async Task GivenIWantToRestoreADeletedProjectWithTheName(string name)
        {
            var idOfTheDesiredProject =
                await _service.Process(new DefineANewProject(name));
            await _service.Process(new ArchiveTheProject(idOfTheDesiredProject));

            _request = new RestoreTheProject(idOfTheDesiredProject);

            IdOfEntityForTearinDown = idOfTheDesiredProject;
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
