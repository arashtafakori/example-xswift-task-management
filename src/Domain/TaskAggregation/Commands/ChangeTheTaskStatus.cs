using XSwift.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    public class ChangeTheTaskStatus :
        RequestToUpdateById<Task, Guid>, IRequest
    {
        public TaskStatus Status { get; private set; }

        public ChangeTheTaskStatus(
            Guid id, 
            TaskStatus status) : base(id)
        {
            Status = status;

            ValidationState.Validate();

        }

        public override async Task<Task> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var task = (await mediator.Send(new GetTheTask(Id)))!;
            task.SetStatus(Status);
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
