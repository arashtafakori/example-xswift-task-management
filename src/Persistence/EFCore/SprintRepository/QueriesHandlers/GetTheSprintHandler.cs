using MediatR;
using XSwift.Datastore;
using Domain.SprintAggregation;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    internal class GetTheSprintHandler :
        IRequestHandler<GetTheSprint, SprintEntity?>
    {
        private readonly Database _database;
        public GetTheSprintHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<SprintEntity?> Handle(
            GetTheSprint request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync<GetTheSprint, SprintEntity>(request: request);
        }
    }
}
