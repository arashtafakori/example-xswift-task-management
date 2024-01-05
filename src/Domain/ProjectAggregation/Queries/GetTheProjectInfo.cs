using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    public class GetTheProjectInfo :
        QueryItemRequestById<ProjectEntity, Guid, ProjectInfo?>
    {
        public GetTheProjectInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
