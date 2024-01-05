using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class GetTheSprintInfoViewModel
    {
        public SprintInfo? SprintInfo { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
    }
}
