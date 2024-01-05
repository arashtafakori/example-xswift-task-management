using Module.Domain.ProjectAggregation;
using Module.Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class GetTheTaskInfoViewModel
    {
        public TaskInfo? TaskInfo { get; set; }
        public ProjectInfo? ProjectInfo { get; set; }
        public List<SelectListItem>? SprintsInfoItems { get; set; }
        public List<SelectListItem>? TaskStatusSelectListItems { get; set; }
    }
}
