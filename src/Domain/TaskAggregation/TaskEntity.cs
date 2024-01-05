using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using System.ComponentModel.DataAnnotations;
using XSwift.Domain;

namespace Module.Domain.TaskAggregation
{
    public class TaskEntity : Entity<TaskEntity, Guid>, IAggregateRoot
    {
        [MaxLengthShouldBe(1000)]
        [StringLength(1000)]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; protected set; } = string.Empty;

        [Required]
        public Guid ProjectId { get; protected set; }
        public Guid? SprintId { get; protected set; }
        public TaskStatus Status { get; protected set; }
        public static TaskEntity New() { return new TaskEntity(); }
        public TaskEntity SetProjectId(Guid value)
        {
            ProjectId = value;

            return this;
        }
        public TaskEntity SetDescription(string value)
        {
            Description = value;

            return this;
        }
        public TaskEntity SetSprintId(Guid? value)
        {
            SprintId = value;

            return this;
        }
        public TaskEntity SetStatus(TaskStatus value)
        {
            Status = value;

            return this;
        }

        public static TaskStatus GetTaskStatusDefaultValue()
        {
            return TaskStatus.NotStarted;
        }

        #region These fields belong to EFCore DbContext configurations
        public ProjectEntity Project { get; private set; }
        public SprintEntity? Sprint { get; private set; }
        #endregion
    }
}
