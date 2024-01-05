using MediatR;
using XSwift.Datastore;
using Module.Domain.TaskAggregation;
using XSwift.Domain;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.TaskRepository
{
    public class GetTaskInfoListHandler :
        IRequestHandler<GetTaskInfoList,
            PaginatedViewModel<TaskInfo>>
    {
        private readonly Database _database;
        public GetTaskInfoListHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<PaginatedViewModel<TaskInfo>> Handle(
            GetTaskInfoList request,
            CancellationToken cancellationToken)
        {
            return await _database.GetPaginatedListAsync(
                request: request,
                selector: (IQueryable<TaskEntity> query) =>
                {
                    return TaskQueryable.SelectAsTaskInfo(_database, query); 
                },
                filter: delegate (IQueryable<TaskEntity> query)
                {
                    return from task in query
                           where
                                (!string.IsNullOrEmpty(request.DescriptionSearchKey) ? task.Description.Contains(request.DescriptionSearchKey!) : true) &&
                                (request.SprintId != null ? task.SprintId == request.SprintId : true) &&
                                (request.Status != null ? task.Status == request.Status : true)
                           select task;
                });
        }
    }
}
