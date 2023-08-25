using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class ArchiveTheProject :
        ArchivingRequestById<ArchiveTheProject, Project, Guid>, IRequest
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

            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}