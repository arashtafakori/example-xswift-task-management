using CoreX.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    public class EditTheTask :
        UpdationRequestById<EditTheTask, Task, Guid>, IRequest
    {
        [BindTo(typeof(Task), nameof(Task.Description))]
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

        public override async Task<Task> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(new GetTheTask(Id));
            entity!.SetDescription(Description)
                .SetSprintId(SprintId)
                .SetStatus(Status);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
