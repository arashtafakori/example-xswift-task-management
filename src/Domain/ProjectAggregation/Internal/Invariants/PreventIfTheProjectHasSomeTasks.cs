using MediatR;
using XSwift.Base;
using XSwift.Domain;

namespace Module.Domain.ProjectAggregation
{
    internal class PreventIfTheProjectHasSomeTasks
        : InvariantRequestById<ProjectEntity, Guid>
    {
        public PreventIfTheProjectHasSomeTasks(Guid id) : base(id)
        {
        }

        public override IIssue? GetIssue()
        {
            return new SomeTasksHaveBeenDefinedForThisProject();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
