using XSwift.Domain;
using MediatR;

namespace Domain.TaskAggregation
{
    public class AddATask
        : RequestToCreate<Task>, IRequest<Guid>
    {
        public Guid ProjectId { get; private set; }

        [BindTo(typeof(Task), nameof(Task.Description))]
        public string Description { get; private set; }

        public Guid? SprintId { get; private set; }
        public TaskStatus Status { get; private set; } = Task.GetTaskStatusDefaultValue();

        public AddATask(
            Guid projectId,
            string description,
            Guid? sprintId = null)
        {
            ProjectId = projectId;
            Description = description.Trim();
            SprintId = sprintId;
            ValidationState.Validate();
        }

        public override async Task<Task> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = Task.New();
            entity.SetProjectId(ProjectId)
                .SetDescription(Description)
                .SetSprintId(SprintId)
                .SetStatus(Status);
            await base.ResolveAsync(mediator, entity!);
            return entity;
        }
    }
}
