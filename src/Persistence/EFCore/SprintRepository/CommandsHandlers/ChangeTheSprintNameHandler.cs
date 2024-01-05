using XSwift.Datastore;
using Module.Domain.SprintAggregation;
using MediatR;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.SprintRepository
{
    public class ChangeTheSprintNameHandler :
        IRequestHandler<ChangeTheSprintName>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ChangeTheSprintNameHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            ChangeTheSprintName request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.UpdateAsync(request, entity);
            return new Unit();
        }
    }
}
