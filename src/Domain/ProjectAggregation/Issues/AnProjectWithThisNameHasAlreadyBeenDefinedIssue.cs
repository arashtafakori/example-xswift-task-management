using CoreX.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.ProjectAggregation
{
    public class AnProjectWithThisNameHasAlreadyBeenDefinedIssue : InvariantIssue
    {
        public AnProjectWithThisNameHasAlreadyBeenDefinedIssue()
        {
            Name = GetType().FullName!;
            Description = string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_AProjectWithThisNameHasAlreadyBeenDefined);
        }
    }
}
