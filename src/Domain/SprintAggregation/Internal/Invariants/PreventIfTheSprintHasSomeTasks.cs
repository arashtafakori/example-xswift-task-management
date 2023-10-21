using MediatR;
using XSwift.Base;
using XSwift.Domain;

namespace Domain.SprintAggregation
{
    internal class PreventIfTheSprintHasSomeTasks
        : InvariantRequestById<SprintEntity, Guid>
    {
        public PreventIfTheSprintHasSomeTasks(Guid id) : base(id)
        {
        }

        public override IIssue? GetIssue()
        {
            return new SomeTasksHaveBeenDefinedForThisSprint();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
