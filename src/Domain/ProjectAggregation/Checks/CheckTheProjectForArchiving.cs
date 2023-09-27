using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class CheckTheProjectForArchiving :
        AnyRequestById<Project, Guid>,
        IRequest
    {
        public CheckTheProjectForArchiving(Guid id)
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
