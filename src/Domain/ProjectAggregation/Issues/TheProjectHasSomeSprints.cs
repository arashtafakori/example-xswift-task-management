using XSwift.Domain;
using Module.Domain.Properties;
using System.Globalization;

namespace Module.Domain.ProjectAggregation
{
    internal class TheProjectHasSomeSprints : InvariantIssue
    {
        public TheProjectHasSomeSprints(
            string description = "") :
            base(outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Invariant_Issue_TheProjectHasSomeSprints))
        {
        }
    }
}
