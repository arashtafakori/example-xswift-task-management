using MediatR;
using CoreX.Datastore;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    public class GetSomeSprintsInfoHandler :
        IRequestHandler<GetSomeSprintInfo,
            List<SprintInfo>>
    {
        private readonly Database _database;
        public GetSomeSprintsInfoHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<List<SprintInfo>> Handle(
            GetSomeSprintInfo request,
            CancellationToken cancellationToken)
        {
            return (await _database.GetEntitiesAsync<
                GetSomeSprintInfo, Sprint>(request: request))
                .ConvertAll(x => x.GetInfo())!;
        }
    }
}
