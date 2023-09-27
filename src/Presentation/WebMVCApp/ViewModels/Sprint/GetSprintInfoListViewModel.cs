using XSwift.Domain;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class GetSprintInfoListViewModel
    {
        public PaginatedViewModel<SprintInfo> SprintInfoList { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
    }
}
