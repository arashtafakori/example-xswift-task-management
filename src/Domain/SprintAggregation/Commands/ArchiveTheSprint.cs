using CoreX.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class ArchiveTheSprint :
        ArchivingRequestById<ArchiveTheSprint, Sprint, Guid>, IRequest
    {
        public ArchiveTheSprint(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var entity = await mediator.Send(
                new RetriveTheSprint(Id, evenArchivedData: true));

            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}