using MediatR;
using XSwift.Datastore;
using Module.Domain.SprintAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.SprintAggregation
{
    public class CheckTheSprintForArchivingHandler :
        IRequestHandler<CheckTheSprintForArchiving, bool>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public CheckTheSprintForArchivingHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<bool> Handle(
            CheckTheSprintForArchiving request,
            CancellationToken cancellationToken)
        {
            var result = await _database.AnyAsync<
                 CheckTheSprintForArchiving, SprintEntity>(request);
            await request.ResolveAsync(_mediator);
            return result;
        }
    }
}
