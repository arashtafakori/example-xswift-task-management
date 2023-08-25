using CoreX.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.SprintAggregation
{
    public class TheStartDateOfTheSprintCanNotBeLaterThanTheEndDate : InvariantIssue
    {
        public TheStartDateOfTheSprintCanNotBeLaterThanTheEndDate()
        {
            Name = GetType().FullName!;
            Description = string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_TheStartDateOfTheSprintCanNotBeLaterThanTheEndDate);
        }
    }
}
