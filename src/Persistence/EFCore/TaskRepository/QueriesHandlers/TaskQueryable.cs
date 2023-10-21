using XSwift.Datastore;
using Domain.TaskAggregation;

namespace Persistence.EFCore.TaskRepository
{
    public class TaskQueryable
    {
        public static IQueryable<TaskInfo> SelectAsTaskInfo(
            IDatabase database,
            IQueryable<TaskEntity> query)
        {
            var dbContext = database.GetDbContext<ModuleDbContext>();
            return from task in query
                   join project in dbContext.Projects on
                   task.ProjectId equals project.Id
                   join sprint in dbContext.Sprints on
                   task.SprintId equals sprint.Id into sprintGroup
                   from groupedItem in sprintGroup.DefaultIfEmpty()
                   select TaskInfo.ToModel(task, project.Name, groupedItem.Id, groupedItem.Name, null, "");
        }
    }
}
