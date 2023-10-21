using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class RestoreTheSprint :
        RequestToRestoreById<SprintEntity, Guid>
    {
        public RestoreTheSprint(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<SprintEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var sprint = (await mediator.Send(
                new GetTheSprint(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
