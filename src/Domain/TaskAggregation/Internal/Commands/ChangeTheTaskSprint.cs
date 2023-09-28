using XSwift.Domain;
using MediatR;
 
namespace Domain.TaskAggregation
{
    internal class ChangeTheTaskSprint :
        RequestToUpdateById<Task, Guid>, IRequest
    {
        public Guid? SprintId { get; private set; }

        public ChangeTheTaskSprint(
            Guid id, 
            Guid? sprintId = null) : base(id)
        {
            SprintId = sprintId;

            ValidationState.Validate();
        }

        public override async Task<Task> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(new GetTheTask(Id));
            entity!.SetSprintId(SprintId);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
