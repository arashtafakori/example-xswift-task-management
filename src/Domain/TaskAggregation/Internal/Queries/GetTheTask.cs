using MediatR;
using XSwift.Domain;

namespace Domain.TaskAggregation
{
    internal class GetTheTask :
        QueryItemRequestById<TaskEntity, Guid, TaskEntity?>
    {
        public GetTheTask(Guid id,
            bool evenArchivedData = false)
            : base(id)
        {
            TrackingMode = true;
            PreventIfNoEntityWasFound = true;
            EvenArchivedData = evenArchivedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
