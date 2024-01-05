using MediatR;
using XSwift.Domain;

namespace Module.Domain.TaskAggregation
{
    public class GetTheTaskInfo :
        QueryItemRequestById<TaskEntity, Guid, TaskInfo?>
    {
        public GetTheTaskInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
