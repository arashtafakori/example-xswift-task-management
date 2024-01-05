using XSwift.Domain;
using MediatR;

namespace Module.Domain.TaskAggregation
{
    public class AddATask
        : RequestToCreate<TaskEntity, Guid>
    {
        public Guid ProjectId { get; private set; }

        [BindTo(typeof(TaskEntity), nameof(TaskEntity.Description))]
        public string Description { get; private set; }

        public Guid? SprintId { get; private set; }
        public TaskStatus Status { get; private set; } = TaskEntity.GetTaskStatusDefaultValue();

        public AddATask(Guid projectId,
            string description, Guid? sprintId = null)
        {
            ProjectId = projectId;
            Description = description.Trim();
            SprintId = sprintId;
            ValidationState.Validate();
        }

        public override async Task<TaskEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var task = TaskEntity.New()
                .SetProjectId(ProjectId)
                .SetDescription(Description)
                .SetSprintId(SprintId)
                .SetStatus(Status);
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
