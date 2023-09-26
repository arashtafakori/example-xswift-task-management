using CoreX.Domain;
using Domain.ProjectAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class GetProjectInfoListViewModel
    {
        public PaginatedViewModel<ProjectInfo> ProjectInfoList { get; set; }
    }
}
