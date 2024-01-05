using XSwift.Domain;
using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class GetSprintInfoListViewModel
    {
        public PaginatedViewModel<SprintInfo> SprintInfoList { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
    }
}
