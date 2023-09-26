using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class RestoreTheProject :
        RestorationRequestById<RestoreTheProject, Project, Guid>, IRequest
    {
        public RestoreTheProject(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true));
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
