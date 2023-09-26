using Domain.ProjectAggregation;
using Domain.SprintAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class GetTheSprintInfoViewModel
    {
        public SprintInfo? SprintInfo { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
    }
}
