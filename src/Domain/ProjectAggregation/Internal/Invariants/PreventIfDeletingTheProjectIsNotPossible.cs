using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    internal class PreventIfDeletingTheProjectIsNotPossible :
        AnyRequestById<Project, Guid>,
        IRequest
    {
        public PreventIfDeletingTheProjectIsNotPossible(Guid id)
            : base(id)
        {
            DefineAPersistentBasedInvariant(
                condition: x => x.Id == Id && x.Sprints.Any(),
                issue: new TheProjectHasSomeSprints(typeof(Project).Name)
                );

            DefineAPersistentBasedInvariant(
                condition: x => x.Id == Id && x.Tasks.Any(),
                issue: new SomeTasksHaveBeenDefinedForThisProject(typeof(Project).Name)
                );
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.CheckAsync(mediator);
        }
    }
}
