using XSwift.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.ProjectAggregation
{
    internal class SomeTasksHaveBeenDefinedForThisProject : InvariantIssue
    {
        public SomeTasksHaveBeenDefinedForThisProject(
            string description = "")
            : base(outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_SomeTasksHaveBeenDefinedForThisProject))
        {
        }
    }
}
