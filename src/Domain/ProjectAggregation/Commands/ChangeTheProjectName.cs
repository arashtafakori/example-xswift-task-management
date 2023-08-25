using CoreX.Domain;
using MediatR;
 

namespace Domain.ProjectAggregation
{
    public class ChangeTheProjectName :
        UpdationRequestById<ChangeTheProjectName, Project, Guid>, IRequest
    {
        [BindTo(typeof(Project), nameof(Project.Name))]
        public string Name { get; private set; }
 
        public ChangeTheProjectName(Guid id, string name) : base(id)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(new RetriveTheProject(Id));
            entity!.SetName(Name);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
