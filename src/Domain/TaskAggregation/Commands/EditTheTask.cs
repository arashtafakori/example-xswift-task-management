using XSwift.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    public class EditTheTask :
        RequestToUpdateById<TaskEntity, Guid>
    {
        [BindTo(typeof(TaskEntity), nameof(TaskEntity.Description))]
        public string Description { get; private set; }
        public Guid? SprintId { get; private set; }
        public TaskStatus Status { get; private set; }

        public EditTheTask(
            Guid id, 
            string description,
            TaskStatus status,
            Guid? sprintId = null) : base(id)
        {
            Description = description.Trim();
            Status = status;
            SprintId = sprintId;

            ValidationState.Validate();
        }

        public override async Task<TaskEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var task = (await mediator.Send(new GetTheTask(Id)))!;
            task.SetDescription(Description)
                .SetSprintId(SprintId)
                .SetStatus(Status);
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
