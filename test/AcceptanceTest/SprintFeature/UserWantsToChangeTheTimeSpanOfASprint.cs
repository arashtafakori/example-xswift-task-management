using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Module.Contract;
using Module.Domain.SprintAggregation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XSwift.Base;
using XSwift.FluentAssertions;
using Xunit;

namespace AcceptanceTest.SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to change the time span of a sprint
    /// So that I can be able to access the sprint with the new time span
    /// </summary>
    public class UserWantsToChangeTheTimeSpanOfASprint : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public UserWantsToChangeTheTimeSpanOfASprint(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Theory]
        [MemberData(nameof(GetDatePairs))]
        internal async Task GivenUserChangesTheTimeSpanOfASprintToANew_AandGivenStartDateOrEndDateIsEarlierThanTheLastTwelveMonths_WhenChangingTheTimeSpan_ThenShouldBePreventedFromChangingIt(
            DateTime startDate, DateTime endDate)
        {
            ISprintService service = _serviceScope.ServiceProvider.GetRequiredService<ISprintService>();

            var projectId = await DataFacilitator.DefineAProject(
                _serviceScope, name: "Task Management");

            var sprintId = await DataFacilitator.DefineASprint(
                _serviceScope, projectId, name: "Sprint 01");

            // Given
            var request = new ChangeTheSprintTimeSpan(sprintId, startDate, endDate);

            // When
            Func<Task> actual = async () => await service.Process(request);

            // Then
            await actual.Should().BeSatisfiedWith<TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths>();

            // Tear down
            _fixture.EnsureRecreatedDatabase();
        }

        private static IEnumerable<object[]> GetDatePairs()
        {
            yield return new object[] { DateTimeHelper.UtcNow.AddMonths(-16), DateTimeHelper.UtcNow.AddMonths(-15) };
            yield return new object[] { DateTimeHelper.UtcNow.AddMonths(-14), DateTimeHelper.UtcNow.AddMonths(-2) };
        }
    }
}
