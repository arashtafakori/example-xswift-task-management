using CoreX.Domain;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using System.ComponentModel.DataAnnotations;

namespace Domain.TaskAggregation
{
    public class Task : Entity<Task, Guid>, IAggregateRoot
    {
        [MaxLengthShouldBe(1000)]
        [StringLength(1000)]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; private set; } = string.Empty;

        [Required]
        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }

        public Guid? SprintId { get; private set; }
        public Sprint? Sprint { get; private set; }
        public TaskStatus Status { get; private set; }

        public static Task New()
        {
            return new Task();
        }
        public Task SetProjectId(Guid value)
        {
            ProjectId = value;

            return this;
        }
        public Task SetDescription(string value)
        {
            Description = value;

            return this;
        }
        public Task SetSprintId(Guid? value)
        {
            SprintId = value;

            return this;
        }
        public Task SetStatus(TaskStatus value)
        {
            Status = value;

            return this;
        }

        public static TaskStatus GetTaskStatusDefaultValue()
        {
            return TaskStatus.NotStarted;
        }
    }
}
