using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class ArchiveTheProject :
        DeletionRequestById<ArchiveTheProject, Project, Guid>, IRequest
    {
        public ArchiveTheProject(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(
                new RetriveTheProject(Id, evenArchivedData: true));

            //await new InvariantState
            //{
            //    new PreventToArchivingIfTheEntityHasBeenArchived<Project>(entity!)
            //}.CheckAsync(mediator);

            base.Resolve(entity!);
            return entity!;
        }
    }
}