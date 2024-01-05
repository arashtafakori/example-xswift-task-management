using XSwift.Domain;
using MediatR;

namespace Module.Domain.SprintAggregation
{
    public class ChangeTheSprintName :
        RequestToUpdateById<SprintEntity, Guid>
    {
        [BindTo(typeof(SprintEntity), nameof(SprintEntity.Name))]
        public string Name { get; private set; }
 
        public ChangeTheSprintName(Guid id, string name)
            : base(id)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<SprintEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var sprint = (await mediator.Send(new GetTheSprint(Id)))!;
            sprint.SetName(Name);
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
