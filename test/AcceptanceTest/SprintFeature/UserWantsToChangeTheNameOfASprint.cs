using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.SprintAggregation;
using System;
using System.Threading.Tasks;
using XSwift.Domain;
using XSwift.FluentAssertions;
using Xunit;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to change the name of a sprint of a project
    /// So that I should be able to access the sprint with the new name
    /// </summary>
    public class UserWantsToChangeTheNameOfASprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public UserWantsToChangeTheNameOfASprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }
        [Fact]
        internal async Task GivenUserChangesTheNameOfASprint_WhenChangineTheName_TheShouldChangeItSuccessfully()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            var newSprintName = "Sprint 02";

            // Given
            var request = new ChangeTheSprintName(sprintId, newSprintName);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
        [Fact]
        internal async Task GivenUserChangesTheNameOfASprint_AndGivenASprintWithTheSameNewNameHasAlreadyExistedForThisProject_WhenChangingTheName_ThenShouldBePreventedFromChangingIt()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            var newSprintName = "Sprint 02";

            // Given
            var request = new ChangeTheSprintName(sprintId, newSprintName);
            await service.Process(new DefineASprint(projectId, newSprintName));

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().BeSatisfiedWith<AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted>();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
