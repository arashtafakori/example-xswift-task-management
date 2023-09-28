using XSwift.Domain;
using MediatR;
 

namespace Domain.ProjectAggregation
{
    public class ChangeTheProjectName :
        RequestToUpdateById<Project, Guid>, IRequest
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
            var project = (await mediator.Send(new GetTheProject(Id)))!;
            project.SetName(Name);
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
