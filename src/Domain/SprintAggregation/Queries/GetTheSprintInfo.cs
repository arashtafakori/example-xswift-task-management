using XSwift.Domain;
using MediatR;

namespace Module.Domain.SprintAggregation
{
    public class GetTheSprintInfo :
        QueryItemRequestById<SprintEntity, Guid, SprintInfo?>
    {
        public GetTheSprintInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
