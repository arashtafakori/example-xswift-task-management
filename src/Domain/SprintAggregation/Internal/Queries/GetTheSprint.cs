using MediatR;
using XSwift.Domain;

namespace Domain.SprintAggregation
{
    internal class GetTheSprint :
        QueryItemRequestById<SprintEntity, Guid, SprintEntity?>
    {

        public GetTheSprint(Guid id,
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
