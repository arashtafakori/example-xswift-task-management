using XSwift.Domain;
using MediatR;

namespace Module.Domain.SprintAggregation
{
    public class DefineASprint
        : RequestToCreate<SprintEntity, Guid>
    {
        public Guid ProjectId { get; private set; }

        [BindTo(typeof(SprintEntity), nameof(SprintEntity.Name))]
        public string Name { get; private set; }
        public DefineASprint(Guid projectId, string name)
            : base()
        {
            ProjectId = projectId;
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<SprintEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var sprint = SprintEntity.New().SetProjectId(ProjectId).SetName(Name);
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
