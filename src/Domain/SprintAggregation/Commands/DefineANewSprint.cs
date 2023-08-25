using CoreX.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class DefineANewSprint
        : CreationRequest<DefineANewSprint, Sprint>, IRequest<Guid>
    {
        [BindTo(typeof(Sprint), nameof(Sprint.ProjectId))]
        public Guid ProjectId { get; private set; }

        [BindTo(typeof(Sprint), nameof(Sprint.Name))]
        public string Name { get; private set; }
        public DefineANewSprint(Guid projectId, string name)
        {
            ProjectId = projectId;
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = Sprint.New();
            entity.SetProjectId(ProjectId).SetName(Name);
            await base.ResolveAsync(mediator, entity!);
            return entity;
        }
    }
}
