using MediatR;
using Module.Domain.TaskAggregation;
using XSwift.Datastore;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.TaskRepository
{
    internal class ChangeTheTaskSprintHandler :
        IRequestHandler<ChangeTheTaskSprint>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ChangeTheTaskSprintHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            ChangeTheTaskSprint request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.UpdateAsync(request, entity);
            return new Unit();
        }
    }
}
