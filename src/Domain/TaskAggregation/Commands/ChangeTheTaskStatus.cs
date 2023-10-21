using XSwift.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    public class ChangeTheTaskStatus :
        RequestToUpdateById<TaskEntity, Guid>
    {
        public TaskStatus Status { get; private set; }

        public ChangeTheTaskStatus(
            Guid id, 
            TaskStatus status) : base(id)
        {
            Status = status;

            ValidationState.Validate();

        }

        public override async Task<TaskEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var task = (await mediator.Send(new GetTheTask(Id)))!;
            task.SetStatus(Status);
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
