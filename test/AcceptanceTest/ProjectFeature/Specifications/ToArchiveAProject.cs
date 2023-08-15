using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.ProjectAggregation;

namespace AcceptanceTest.TaskModule
{
    internal class ToArchiveAProject
    {
        private readonly IProjectService _service;
        private ArchiveTheProject? _request = null;
        private Func<Task>? _actual = null;

        internal ToArchiveAProject(IServiceScope scope)
        {
            _service = scope.ServiceProvider.GetRequiredService<IProjectService>();
        }
        internal async Task GivenIWantToArchiveADesiredProjectWithTheName(string name)
        {
            var idOfTheDesiredProject =
                await _service.Process(new DefineANewProject(name));

            _request = new ArchiveTheProject(idOfTheDesiredProject);
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
