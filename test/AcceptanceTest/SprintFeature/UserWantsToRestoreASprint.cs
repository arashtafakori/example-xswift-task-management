using Module.Contract;
using Module.Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System;
using FluentAssertions;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to restore an archived sprint
    /// So that I should be able to access the sprint again
    /// </summary>
    public class UserWantsToRestoreASprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public UserWantsToRestoreASprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserRestoresAnArchivedSprint_WhenRestoringSprint_ThenShouldRestoreItSuccessfully()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
    
            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new ArchiveTheSprint(sprintId));

            // Given
            var request = new RestoreTheSprint(sprintId);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
