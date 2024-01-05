using MediatR;
using XSwift.Datastore;
using Module.Domain.TaskAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.TaskRepository
{
    public class GetTheTaskInfoHandler :
        IRequestHandler<GetTheTaskInfo, TaskInfo?>
    {
        private readonly Database _database;
        public GetTheTaskInfoHandler(IDatabase database) 
        {
            _database = (Database)database;
        }

        public async Task<TaskInfo?> Handle(
            GetTheTaskInfo request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync(request, selector: (IQueryable<TaskEntity> query) =>
                {
                    return TaskQueryable.SelectAsTaskInfo(_database, query);
                });
        }
    }
}
