using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class DefineAProject
        : RequestToCreate<Project>, IRequest<Guid>
    {
        [BindTo(typeof(Project), nameof(Project.Name))]
        public string Name { get; private set; }

        public DefineAProject(string name)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Project> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var project = Project.New().SetName(Name);
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
