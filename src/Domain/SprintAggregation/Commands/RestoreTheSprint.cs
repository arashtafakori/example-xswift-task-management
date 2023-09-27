using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class RestoreTheSprint :
        RestorationRequestById<RestoreTheSprint, Sprint, Guid>, IRequest
    {
        public RestoreTheSprint(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(
                new GetTheSprint(Id, evenArchivedData: true));
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
