using XSwift.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.SprintAggregation
{
    public class TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths : InvariantIssue
    {
        public TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths(
            string description = "")
        {
            Provide<TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths>(
                outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths));
        }
    }
}
