using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class DeleteTheProject :
        HardDeletionRequestById<DeleteTheProject, Project, Guid>, IRequest
    {
        public DeleteTheProject(Guid id) : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<Project> ResolveAndGetEntityAsync(IMediator mediator)
        {
            var entity = await mediator.Send( new RetriveTheProject(Id, evenArchivedData: true));
            base.Resolve(entity!);
            return entity!;
        }
    }
}
