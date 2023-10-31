using MediatR;
using XSwift.Datastore;
using Domain.TaskAggregation;
using EntityFrameworkCore.XSwift;

namespace Persistence.EFCore.TaskRepository
{
    internal class SetTheTasksOfTheSprintToNoSprintHandler :
        IRequestHandler<SetTheTasksOfTheSprintToNoSprint>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public SetTheTasksOfTheSprintToNoSprintHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            SetTheTasksOfTheSprintToNoSprint request,
            CancellationToken cancellationToken)
        {
            var tasksIds = await _database.GetListAsync(
                           request: request,
                           selector: (IQueryable<TaskEntity> query) => {
                               return from task in query
                                      select task.Id; 
                           });

            await request.NextAsync(_mediator, tasksIds);
            return new Unit();
        }
    }
}
