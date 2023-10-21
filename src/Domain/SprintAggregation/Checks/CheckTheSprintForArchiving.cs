using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class CheckTheSprintForArchiving :
        AnyRequestById<SprintEntity, Guid>
    {
        public CheckTheSprintForArchiving(Guid id)
            : base(id)
        {
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            InvariantState.AddAnInvariantRequest(new PreventIfTheSprintHasSomeTasks(id: Id));
            await InvariantState.AssestAsync(mediator);
        }
    }
}
