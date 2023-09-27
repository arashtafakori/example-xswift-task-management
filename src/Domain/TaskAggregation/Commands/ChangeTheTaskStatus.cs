using XSwift.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    public class ChangeTheTaskStatus :
        UpdationRequestById<EditTheTask, Task, Guid>, IRequest
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
            var entity = await mediator.Send(new GetTheTask(Id));
            entity!.SetStatus(Status);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
