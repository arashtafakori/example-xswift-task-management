using XSwift.Domain;
using MediatR;

namespace Module.Domain.ProjectAggregation
{
    public class DefineAProject
        : RequestToCreate<ProjectEntity, Guid>
    {
        [BindTo(typeof(ProjectEntity), nameof(ProjectEntity.Name))]
        public string Name { get; private set; }

        public DefineAProject(string name) 
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<ProjectEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var project = ProjectEntity.New().SetName(Name);
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
