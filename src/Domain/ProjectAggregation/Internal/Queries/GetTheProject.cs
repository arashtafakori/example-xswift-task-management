using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    internal class GetTheProject :
        QueryItemRequestById<ProjectEntity, Guid, ProjectEntity?>
    {
        public GetTheProject(Guid id,
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
