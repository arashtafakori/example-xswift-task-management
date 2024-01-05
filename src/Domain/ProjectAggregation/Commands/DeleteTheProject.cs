using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    public class DeleteTheProject :
        RequestToDeleteById<ProjectEntity, Guid>
    {
        public DeleteTheProject(Guid id)
            : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<ProjectEntity> ResolveAndGetEntityAsync(IMediator mediator)
        {
            InvariantState.AddAnInvariantRequest(new PreventIfTheProjectHasSomeSprints(id: Id));
            InvariantState.AddAnInvariantRequest(new PreventIfTheProjectHasSomeTasks(id: Id));
            await InvariantState.AssestAsync(mediator);

            var project = (await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
