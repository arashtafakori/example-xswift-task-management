using AcceptanceTest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestStack.BDDfy;
using XSwift.Base;
using Xunit;

namespace SprintFeature
{
    /// <summary>
    /// As a user
    /// I want to change the start span of a sprint
    /// So that I can do the request
    /// </summary>
    public class AsAUserIWantToChangeTheTimeSpanOfASprintSoThatICanDoTheRequest : IClassFixture<SprintFixture>
    {
        private IServiceScope _serviceScope;
        private readonly SprintFixture _fixture;
        public AsAUserIWantToChangeTheTimeSpanOfASprintSoThatICanDoTheRequest(SprintFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Theory]
        [MemberData(nameof(GetDatePairs))]
        internal async Task ToChangeTheTimeSpanOfASprintToANewWhileStartDateAndEndDateIsEarlierThanTheLastTwelveMonths(
            DateTime startDate, DateTime endDate)
        {
            var steps = new ToChangeTheTimeSpanOfASprintToANewWhileStartDateAndEndDateIsEarlierThanTheLastTwelveMonths(_serviceScope!);

            var dataFacilitator = new ApplicationServiceFacilitator(_serviceScope);
            var projectId = await dataFacilitator.DefineAProject(
                projectName: "Task Managment");
            var sprintId = await dataFacilitator.DefineASprint(
                projectId, sprintName: "Sprint 01");

            steps.Given(_ => steps.GivenIWantToChangeTheTimeSpanOfASprintToANewTimeSpan(sprintId, startDate, endDate))
                .Given(_ => steps.AndGivenTheStartDateAndTheEndDateIsEarlierThanTheLastTwelveMonths())
                .When(_ => steps.WhenIRequestIt())
                .Then(_ => steps.ThenTheRequestSholudBeDenied())
                .TearDownWith(_ => _fixture.ResetDbContext())
                .BDDfy();
        }

        private static IEnumerable<object[]> GetDatePairs()
        {
            yield return new object[] { DateTimeHelper.UtcNow.AddMonths(-16), DateTimeHelper.UtcNow.AddMonths(-15) };
            yield return new object[] { DateTimeHelper.UtcNow.AddMonths(-14), DateTimeHelper.UtcNow.AddMonths(-2) };
        }
    }
}
