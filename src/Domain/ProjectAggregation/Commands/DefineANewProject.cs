using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class DefineANewProject
        : CreationRequest<DefineANewProject, Project>, IRequest<Guid>
    {
        [BindTo(typeof(Project), nameof(Project.Name))]
    public string Name { get; private set; }

        public DefineANewProject(string name)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = Project.New();
            entity.SetName(Name);
            await base.ResolveAsync(mediator, entity!);
            return entity;
        }
    }
}
