using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class CheckTheProjectForArchiving :
        AnyRequestById<ProjectEntity, Guid>
    {
        public CheckTheProjectForArchiving(Guid id)
            : base(id)
        {
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            InvariantState.AddAnInvariantRequest(new PreventIfTheProjectHasSomeSprints(id: Id));
            InvariantState.AddAnInvariantRequest(new PreventIfTheProjectHasSomeTasks(id: Id));
            await InvariantState.AssestAsync(mediator);
        }
    }
}
