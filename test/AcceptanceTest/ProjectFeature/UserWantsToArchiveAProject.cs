using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTest.ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to archive a project
    /// So that I should not be able to access the project
    /// </summary>
    public class UserWantsToArchiveAProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public UserWantsToArchiveAProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserArchivesAProject_WhenArchivingProject_ThenShouldRestoreItSuccessfully()
        {
            var service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            // Given
            var request = new ArchiveTheProject(projectId);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
