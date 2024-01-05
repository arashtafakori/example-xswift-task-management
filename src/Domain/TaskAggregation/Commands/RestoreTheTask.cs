using XSwift.Domain;
using MediatR;

namespace Module.Domain.TaskAggregation
{
    public class RestoreTheTask :
        RequestToRestoreById<TaskEntity, Guid>
    {
        public RestoreTheTask(Guid id)
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<TaskEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var task = (await mediator.Send(
                new GetTheTask(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
