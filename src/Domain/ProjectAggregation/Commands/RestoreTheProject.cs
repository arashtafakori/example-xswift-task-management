using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    public class RestoreTheProject :
        RequestToRestoreById<ProjectEntity, Guid>
    {
        public RestoreTheProject(Guid id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<ProjectEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var project = (await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
