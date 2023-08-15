using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class RestoreTheProject :
        UnDeletionRequestById<RestoreTheProject, Project, Guid>, IRequest
    {
        public RestoreTheProject(Guid id) : base(id)
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
            //    new PreventToRestoringIfTheEntityHasNotBeenArchived<Project>(entity!)
            //}.CheckAsync(mediator);

            base.Resolve(entity!);
            return entity!;
        }
    }
}
