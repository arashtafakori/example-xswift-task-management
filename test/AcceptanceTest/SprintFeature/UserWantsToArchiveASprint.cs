using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.SprintAggregation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to archive a sprint
    /// So that I shold not be able to access the sprint
    /// </summary>
    public class UserWantsToArchiveASprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public UserWantsToArchiveASprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserArchivesASprint_WhenArchivingSprint_ThenShouldRestoreItSuccessfully()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            // Given
            var request = new ArchiveTheSprint(sprintId);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
