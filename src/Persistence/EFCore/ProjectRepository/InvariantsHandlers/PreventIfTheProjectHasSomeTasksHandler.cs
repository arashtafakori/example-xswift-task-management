using MediatR;
using Domain.ProjectAggregation;
using XSwift.Datastore;
using EntityFrameworkCore.XSwift;

namespace Persistence.EFCore.ProjectRepository
{
    internal class PreventIfTheProjectHasSomeTasksHandler :
        IRequestHandler<PreventIfTheProjectHasSomeTasks, bool>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public PreventIfTheProjectHasSomeTasksHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<bool> Handle(
            PreventIfTheProjectHasSomeTasks request,
            CancellationToken cancellationToken)
        {
            request.WhereExpression.And(x => x.Id == request.Id && x.Tasks.Any());

            var result = await _database.AnyAsync<
                PreventIfTheProjectHasSomeTasks, ProjectEntity>(request);
            await request.ResolveAsync(_mediator);
            return result;
        }
    }
}
