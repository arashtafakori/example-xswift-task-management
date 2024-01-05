using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using Module.Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Module.Presentation.WebMVCApp.ViewModels
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
        public static EditTheTaskViewModel ToViewModel(TaskInfo info)
        {
            return new EditTheTaskViewModel()
            {
                Id = info.Id,
                Description = info.Description,
                SprintId = info.SprintId,
                Status = info.Status,
                TaskInfo = info
            };
        }

        public EditTheTask ToRequest()
        {
            return new EditTheTask(Id, Description, Status, SprintId);
        }
    }
}
