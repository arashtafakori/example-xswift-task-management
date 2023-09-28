using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class DeleteTheProject :
        RequestToDeleteById<Project, Guid>, IRequest
    {
        public DeleteTheProject(Guid id) : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<Project> ResolveAndGetEntityAsync(IMediator mediator)
        {
            await mediator.Send(new PreventIfDeletingTheProjectIsNotPossible(Id));

            var project = (await mediator.Send(
                new GetTheProject(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
