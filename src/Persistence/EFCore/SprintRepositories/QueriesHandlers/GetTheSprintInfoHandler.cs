using MediatR;
using CoreX.Datastore;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    public class GetTheSprintInfoHandler :
        IRequestHandler<GetTheSprintInfo, SprintInfo?>
    {
        private readonly Database _database;
        public GetTheSprintInfoHandler(IDatabase database) 
        {
            _database = (Database)database;
        }

        public async Task<SprintInfo?> Handle(
            GetTheSprintInfo request,
            CancellationToken cancellationToken)
        {
            return (await _database.GetEntityAsync<
                GetTheSprintInfo, Sprint>(request: request)).
                GetInfo();
        }
    }
}
