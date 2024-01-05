using XSwift.Datastore;
using Module.Domain.SprintAggregation;

namespace Module.Persistence.SprintRepository
{
    public class SprintQueryable
    {
        public static IQueryable<SprintInfo> SelectAsSprintInfo(
            IDatabase database,
            IQueryable<SprintEntity> query)
        {
            var dbContext = database.GetDbContext<ModuleDbContext>();
            return from sprint in query
                   join project in dbContext.Projects on
                   sprint.ProjectId equals project.Id into projectGroup
                   from groupedItem in projectGroup.DefaultIfEmpty()
                   select SprintInfo.ToModel(
                       sprint,
                       groupedItem.Name);
        }
    }
}
