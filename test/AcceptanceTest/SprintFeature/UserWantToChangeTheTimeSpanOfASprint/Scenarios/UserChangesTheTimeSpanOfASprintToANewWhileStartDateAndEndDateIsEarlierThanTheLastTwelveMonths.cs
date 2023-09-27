using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Contract;
using Domain.SprintAggregation;
using CoreX.TestingFacilities;

namespace SprintFeature
{
    internal class UserChangesTheTimeSpanOfASprintToANewWhileStartDateAndEndDateIsEarlierThanTheLastTwelveMonths
    {
        private readonly ISprintService _service;
        private ChangeTheSprintTimeSpan? _request = null;
        private Func<Task>? _actual = null;

        internal UserChangesTheTimeSpanOfASprintToANewWhileStartDateAndEndDateIsEarlierThanTheLastTwelveMonths(IServiceScope serviceScope)
        {
            _service = serviceScope.ServiceProvider.GetRequiredService<ISprintService>();
        }
        internal void GivenIWantToChangeTheTimeSpanOfASprintToANewTimeSpan(
            Guid sprintId, DateTime startDate, DateTime endDate)
        {
            _request = new ChangeTheSprintTimeSpan(sprintId, startDate, endDate);
        }
        internal void AndGivenTheStartDateAndTheEndDateIsEarlierThanTheLastTwelveMonths()
        {
        }
        internal void WhenIRequestIt()
        {
            _actual = async () => await _service.Process(_request!);
        }
        internal async Task ThenTheRequestSholudBeDenied()
        {
            await _actual.Should().BeSatisfiedWith<TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths>();
        }
    }
}
