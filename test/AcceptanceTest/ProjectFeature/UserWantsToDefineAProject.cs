using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.ProjectAggregation;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using XSwift.Domain;
using XSwift.FluentAssertions;

namespace AcceptanceTest.ProjectFeature
{
    /// <summary>
    /// As a user
    /// I want to define a project
    /// So that I should be able to access the project
    /// </summary>
    public class UserWantsToDefineAProject : IClassFixture<ProjectFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProjectFixture _fixture;
        public UserWantsToDefineAProject(ProjectFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        internal async Task GivenUserDefinesAProject_WhenDefiningProject_ThenShouldDefineItSuccessfully()
        {
            IProjectService service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectName = "Task Management";

            // Given
            var request = new DefineAProject(projectName);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().NotThrowAsync();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
        [Fact]
        internal async Task GivenUserDefinesAProject_AndGivenAProjectWithThisNameHasAlreadyExisted_WhenDefiningProject_ThenShouldBePreventedFromDefiningIt()
        {
            IProjectService service = _serviceScope!.ServiceProvider.GetRequiredService<IProjectService>();

            var projectName = "Task Management";

            // Given
            var request = new DefineAProject(projectName);
            await service.Process(new DefineAProject(projectName));

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().BeSatisfiedWith<AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted>();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }
    }
}
