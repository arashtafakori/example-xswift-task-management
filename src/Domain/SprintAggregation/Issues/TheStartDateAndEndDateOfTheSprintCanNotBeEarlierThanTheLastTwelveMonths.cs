using CoreX.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.SprintAggregation
{
    public class TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths : InvariantIssue
    {
        public TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths()
        {
            Name = GetType().FullName!;
            Description = string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths);
        }
    }
}
