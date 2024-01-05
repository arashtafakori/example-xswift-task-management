using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    public class ChangeTheProjectName :
        RequestToUpdateById<ProjectEntity, Guid>
    {
        [BindTo(typeof(ProjectEntity), nameof(ProjectEntity.Name))]
        public string Name { get; private set; }
 
        public ChangeTheProjectName(Guid id, string name) 
            : base(id)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<ProjectEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var project = (await mediator.Send(new GetTheProject(Id)))!;
            project.SetName(Name);
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
