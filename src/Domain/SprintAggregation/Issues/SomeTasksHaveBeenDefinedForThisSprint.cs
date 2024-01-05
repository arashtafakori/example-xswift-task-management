using XSwift.Domain;
using Module.Domain.ProjectAggregation;
using Module.Domain.Properties;
using System.Globalization;

namespace Module.Domain.SprintAggregation
{
    internal class SomeTasksHaveBeenDefinedForThisSprint : InvariantIssue
    {
        public SomeTasksHaveBeenDefinedForThisSprint(
           string description = "") :
            base(outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_SomeTasksHaveBeenDefinedForThisSprint))
        {
        }
    }
}
