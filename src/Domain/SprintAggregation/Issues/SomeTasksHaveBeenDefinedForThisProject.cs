using CoreX.Domain;
using Domain.ProjectAggregation;
using Domain.Properties;
using System.Globalization;

namespace Domain.SprintAggregation
{
    internal class SomeTasksHaveBeenDefinedForThisSprint : InvariantIssue
    {
        public SomeTasksHaveBeenDefinedForThisSprint(
           string description = "")
        {
            Provide<SomeTasksHaveBeenDefinedForThisProject>(
                outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_SomeTasksHaveBeenDefinedForThisSprint));
        }
    }
}
