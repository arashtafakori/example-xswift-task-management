using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class ArchiveTheProject :
        RequestToArchiveById<Project, Guid>, IRequest
    {
        public ArchiveTheProject(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await mediator.Send(new PreventIfDeletingTheProjectIsNotPossible(id: Id));

            var project = (await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}