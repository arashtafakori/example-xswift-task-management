using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class RestoreTheProject :
        RequestToRestoreById<Project, Guid>, IRequest
    {
        public RestoreTheProject(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var project = (await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
