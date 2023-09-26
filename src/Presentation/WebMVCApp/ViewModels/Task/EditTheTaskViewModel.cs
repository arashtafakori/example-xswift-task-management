using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebMVCApp.ViewModels
{
    public class EditTheTaskViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Sprint")]
        public Guid? SprintId { get; set; }
        public TaskInfo? TaskInfo { get; set; }
        [DisplayName("Task Status")]
        public Domain.TaskAggregation.TaskStatus Status { get; set; }
        public List<SelectListItem>? SprintsInfoItems { get; set; }
        public List<SelectListItem>? TaskStatusSelectListItems { get; set; }
        public static EditTheTaskViewModel ToViewModel(TaskInfo taskInfo)
        {
            return new EditTheTaskViewModel()
            {
                Id = taskInfo.Id,
                Description = taskInfo.Description,
                SprintId = taskInfo.SprintId,
                Status = taskInfo.Status,
                TaskInfo = taskInfo
            };
        }

        public EditTheTask ToRequest()
        {
            return new EditTheTask(Id, Description, Status, SprintId);
        }
    }
}
