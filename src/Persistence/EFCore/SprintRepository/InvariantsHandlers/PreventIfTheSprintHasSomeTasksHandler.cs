using MediatR;
using XSwift.Datastore;
using Module.Domain.SprintAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.SprintRepository
{
    internal class PreventIfTheSprintHasSomeTasksHandler :
        IRequestHandler<PreventIfTheSprintHasSomeTasks, bool>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public PreventIfTheSprintHasSomeTasksHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<bool> Handle(
            PreventIfTheSprintHasSomeTasks request,
            CancellationToken cancellationToken)
        {
            request.WhereExpression.And(x => x.Id == request.Id && x.Tasks.Any());

            var result = await _database.AnyAsync<
                PreventIfTheSprintHasSomeTasks, SprintEntity>(request);
            await request.ResolveAsync(_mediator);
            return result;
        }
    }
}
