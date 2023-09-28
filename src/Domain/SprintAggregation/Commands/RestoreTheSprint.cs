using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class RestoreTheSprint :
        RequestToRestoreById<Sprint, Guid>, IRequest
    {
        public RestoreTheSprint(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var sprint = (await mediator.Send(
                new GetTheSprint(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
