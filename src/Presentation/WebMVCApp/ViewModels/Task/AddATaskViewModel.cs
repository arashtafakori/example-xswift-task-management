using Domain.ProjectAggregation;
using Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebMVCApp.ViewModels
{
    public class AddATaskViewModel
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Sprint")]
        public Guid? SprintId { get; set; }

        [DisplayName("Task Status")]
        public Domain.TaskAggregation.TaskStatus Status { get; set; } =
            Domain.TaskAggregation.Task.GetTaskStatusDefaultValue();

        public ProjectInfo? ProjectInfo { get; set; }
        public List<SelectListItem>? SprintsInfoItems { get; set; }
        public List<SelectListItem>? TaskStatusSelectListItems { get; set; }
        public AddATask ToRequest()
        {
            return new AddATask(ProjectId, Description, SprintId);
        }
    }
}
