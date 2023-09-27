using CoreX.Domain;
using Domain.Properties;
using System.Globalization;

namespace Domain.ProjectAggregation
{
    internal class TheProjectHasSomeSprints : InvariantIssue
    {
        public TheProjectHasSomeSprints(
            string description = "")
        {
            Provide<TheProjectHasSomeSprints>(
                outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_TheProjectHasSomeSprints));
        }
    }
}
