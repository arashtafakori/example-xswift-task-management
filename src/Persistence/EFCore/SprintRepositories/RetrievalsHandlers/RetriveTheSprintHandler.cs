using MediatR;
using CoreX.Datastore;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    public class RetriveTheSprintHandler :
        IRequestHandler<RetriveTheSprint, Sprint?>
    {
        private readonly Database _database;
        public RetriveTheSprintHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<Sprint?> Handle(
            RetriveTheSprint request,
            CancellationToken cancellationToken)
        {
            return await _database.GetEntityAsync<
                RetriveTheSprint, Sprint>(request: request);
        }
    }
}
