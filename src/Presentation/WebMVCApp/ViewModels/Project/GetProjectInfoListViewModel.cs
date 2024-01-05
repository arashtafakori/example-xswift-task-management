using XSwift.Domain;
using Module.Domain.ProjectAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class GetProjectInfoListViewModel
    {
        public PaginatedViewModel<ProjectInfo> ProjectInfoList { get; set; }
    }
}
