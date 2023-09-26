using Domain.ProjectAggregation;
using Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.WebMVCApp.ViewModels
{
    public class GetTheTaskInfoViewModel
    {
        public TaskInfo? TaskInfo { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
        public List<SelectListItem>? SprintsInfoItems { get; set; }
        public List<SelectListItem>? TaskStatusSelectListItems { get; set; }
    }
}
