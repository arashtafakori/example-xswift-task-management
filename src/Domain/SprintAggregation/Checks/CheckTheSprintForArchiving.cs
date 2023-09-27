using XSwift.Domain;
using Domain.ProjectAggregation;
using MediatR;

namespace Domain.SprintAggregation
{
    public class CheckTheSprintForArchiving :
        AnyRequestById<Sprint, Guid>,
        IRequest
    {
        public CheckTheSprintForArchiving(Guid id)
            : base(id)
        {

            DefineAPersistentBasedInvariant(
                condition: x => x.Id == Id && x.Tasks.Any(),
                issue: new SomeTasksHaveBeenDefinedForThisProject()
                );
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.CheckAsync(mediator);
        }
    }
}
