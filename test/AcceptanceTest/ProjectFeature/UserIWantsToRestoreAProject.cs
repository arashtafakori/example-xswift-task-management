using Module.Contract;
using Module.Domain.ProjectAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System;
using FluentAssertions;

namespace AcceptanceTest.ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to restore a project
    /// So that I should be able to access the project again
    /// </summary>
    public class UserWantsToRestoreAProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public UserWantsToRestoreAProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserRestoresAnArchivedProject_WhenRestoringProject_ThenShouldRestoreItSuccessfully()
        {
            var service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new ArchiveTheProject(projectId));

            // Given
            var request = new RestoreTheProject(projectId);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
