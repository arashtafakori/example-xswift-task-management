using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class DefineASprint
        : RequestToCreate<Sprint>, IRequest<Guid>
    {
        public Guid ProjectId { get; private set; }

        [BindTo(typeof(Sprint), nameof(Sprint.Name))]
        public string Name { get; private set; }
        public DefineASprint(Guid projectId, string name)
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
