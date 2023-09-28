using XSwift.Domain;
using MediatR;
 

namespace Domain.SprintAggregation
{
    public class ChangeTheSprintName :
        RequestToUpdateById<Sprint, Guid>, IRequest
    {
        [BindTo(typeof(Sprint), nameof(Sprint.Name))]
        public string Name { get; private set; }
 
        public ChangeTheSprintName(Guid id, string name) : base(id)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var sprint = (await mediator.Send(new GetTheSprint(Id)))!;
            sprint.SetName(Name);
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
