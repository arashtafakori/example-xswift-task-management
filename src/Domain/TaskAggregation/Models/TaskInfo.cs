using XSwift.Base;
using XSwift.Domain;
using Domain.Properties;
using System.ComponentModel.DataAnnotations;

namespace Domain.TaskAggregation
{
    public class TaskInfo : ViewModel, IModifiedViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        public required string Description { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
        [Display(Name = "Project")]
        public required string ProjectName { get; set; }
        public Guid? SprintId { get; set; }
        [Display(Name = "Sprint")]
        public string? SprintName { get; set; }
        public TaskStatus Status { get; set; }
        [Display(Name = "Status")]
        public required string StatusName { get; set; }
        public Guid? OwnerId { get; set; }
        [Display(Name = "Owner")]
        public string? OwnerName { get; set; }

        public static TaskInfo ToModel(
            TaskEntity task,
            string? projectName = "",
            Guid? sprintId = null,
            string? sprintName = null,
            Guid? ownerId = null,
            string? ownerName = null)
        {
            return new TaskInfo()
            {
                Id = task!.Id,
                ProjectId = task.ProjectId,
                Description = task.Description,
                ModifiedDate = task.ModifiedDate,
                SprintId = sprintId,
                ProjectName = projectName!,
                SprintName = sprintName,
                Status = task.Status,
                StatusName = EnumHelper.GetEnumMemberResourceValue<TaskStatus>(
                    Resource.ResourceManager, task.Status!),
                OwnerId = ownerId,
                OwnerName = ownerName,
            };
        }
    }
}
