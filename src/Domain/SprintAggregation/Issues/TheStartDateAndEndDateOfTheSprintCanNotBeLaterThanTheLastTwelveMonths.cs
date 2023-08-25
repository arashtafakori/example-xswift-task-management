using CoreX.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.SprintAggregation
{
    public class TheStartDateAndEndDateOfTheSprintCanNotBeLaterThanTheLastTwelveMonths : InvariantIssue
    {
        public TheStartDateAndEndDateOfTheSprintCanNotBeLaterThanTheLastTwelveMonths()
        {
            Name = GetType().FullName!;
            Description = string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_TheStartDateAndEndDateOfTheSprintCanNotBeLaterThanTheLastTwelveMonths);
        }
    }
}
