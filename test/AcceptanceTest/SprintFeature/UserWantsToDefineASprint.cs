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
    /// I want to define a sprint to a project
    /// So that I should be able to access the sprint
    /// </summary>
    public class UserWantsToDefineASprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public UserWantsToDefineASprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserDefinesASprint_WhenDefiningSprint_ThenShouldDefineItSuccessfully()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintName = "Sprint 01";


            // Given
            var request = new DefineASprint(projectId, sprintName);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }

        [Fact]
        internal async Task GivenUserDefinesASprint_AndGivenASprintWithThisNameHasAlreadyExistedForThisProject_WhenDefiningSprint_ThenShouldBePreventedFromDefiningIt()
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintName = "Sprint 01";

            // Given
            var request = new DefineASprint(projectId, sprintName);
            await service.Process(new DefineASprint(projectId, sprintName));

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().BeSatisfiedWith<AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted>();

            // Tear Down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
