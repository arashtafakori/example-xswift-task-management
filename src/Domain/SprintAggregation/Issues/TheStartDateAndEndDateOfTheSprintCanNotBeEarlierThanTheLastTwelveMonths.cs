using XSwift.Domain;
using Module.Domain.Properties;
using System.Globalization;

namespace Module.Domain.SprintAggregation
{
    public class TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths : InvariantIssue
    {
        public TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths(
            string description = "") : base(outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths))
        {
        }
    }
}
