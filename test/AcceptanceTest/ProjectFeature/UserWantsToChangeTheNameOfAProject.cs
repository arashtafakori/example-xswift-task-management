using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using System;
using System.Threading.Tasks;
using XSwift.Domain;
using XSwift.FluentAssertions;
using Xunit;

namespace AcceptanceTest.ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to change the name of a project
    /// So that I should be able to access the project with the new name
    /// </summary>
    public class UserWantsToChangeTheNameOfAProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public UserWantsToChangeTheNameOfAProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        
        internal async Task GivenUserChangesTheNameOfAProject_WhenChangingTheName_ThenShouldChangeItSuccessfully()
        {
            var service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var newProjectName = "Task Board";

            // Given
            var request = new ChangeTheProjectName(projectId, newProjectName);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();
 
            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }

        [Fact]
        internal async Task GivenUserChangesTheNameOfAProject_AndGivenAProjectWithTheSameNewNameHasAlreadyExisted_WhenChangingTheName_ThenShouldBePreventedFromChangingIt()
        {
            var service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var newProjectName = "Task Board";

            // Given
            var request = new ChangeTheProjectName(projectId, newProjectName);
            await service.Process(new DefineAProject(newProjectName));

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().BeSatisfiedWith<AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted>();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
